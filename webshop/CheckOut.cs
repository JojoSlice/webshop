using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using webshop.Helpers;
using webshop.Models;

namespace webshop
{
    internal class CheckOut
    {
        List<string> customerInfo = new List<string>();
        public bool readyForPayment = false;
        public Models.Order order = new Models.Order();
        public float totalPrice;
        public void Run() 
        {
            using var db = new MyDbContext();
            GetOrder(db);
            bool checkOut = false;
            var cart = new ViewCart();

            do
            {
                Console.Clear();
                CartBox(cart);
                Console.WriteLine("Are you happy with your cart? Y/N");
                var happy = Console.ReadKey();
                if (happy.Key == ConsoleKey.N)
                {
                    cart.ManageCart();
                }
                Console.Clear();
                CartBox(cart);
                InfoBox(db);
                Console.WriteLine("");
                if (readyForPayment)
                {
                    PaymentAndShipping();
                    checkOut = true;
                }
            }while(!checkOut);
            Console.Clear();
            CartBox(cart);
            Confirm(db);
            
        }

        public void GetOrder(MyDbContext db)
        {

            var garments = TempCart.GetTempCart()
                .OfType<Models.Garment>()
                .ToList();
            foreach (var garment in garments)
            {
                Models.Garment garmentInfo = db.Garment
                    .FirstOrDefault(x => x.Id == garment.Id);

                order.Garments.Add(garmentInfo);

                if (garment.Stock > 0)
                {
                    garment.Stock -= 1;
                }
                else
                {
                    throw new InvalidOperationException($"The product {garment.Name} is out of stock!");
                }
            }

                totalPrice += garments.Sum(p => p.Price * p.SaleTT);
        }

        public void CartBox(ViewCart cart)
        {
            var cartList = cart.View();

            cartList.Add($"Total price: {Math.Round(totalPrice, 2)}$");

            var boxWindow = new WindowManager("Cart", 0, 0, cartList);
            boxWindow.Draw();
        }
        public void InfoBox(MyDbContext db)
        {
            var infoWindow = new WindowManager("Your information", 0, 10, customerInfo);

            if (Account.Loggedin && !Account.Admin)
            {

                    var customer = db.Customer.FirstOrDefault(c => c.Id == Account.LoggedinID);

                    customerInfo.AddRange(new List<string>
                    {
                        customer.Name,
                        customer.Gender,
                        customer.Email,
                        customer.PhoneNumber ?? "No phone number",
                        customer.Address,
                        customer.City,
                        customer.Region,
                        customer.PostalCode
                    });
                    order.CustomerId = customer.Id;
                infoWindow.Draw();
                readyForPayment = true;
            }
            else
            {
                customerInfo.AddRange(new List<string>
                {
                    "Press 1 to Log in",
                    "Press 2 to Register",
                    "Press 3 to shop as guest"
                });

                infoWindow.Draw();

                var key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        var login = new LogIn();
                        login.Log();
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        var reg = new Reg();
                        reg.ReggIn();
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        EnterInfo();
                        break;
                }
                readyForPayment = true;
            }

        }

        public void PaymentAndShipping()
        {
            var paymentOptions = new List<string>
            {
                "1: Klarna. Pay later. 10$",
                "2: Creditcard"
            };
            var payWindow = new WindowManager("Payment options", 40, 15, paymentOptions);

            var shippingOptions = new List<string>
            {
                "1: Pick up at local dealer.  10$",
                "2: Delivery to door.         50$"
            };
            var shippingWindow = new WindowManager("Shipping options", 40, 19, shippingOptions);

            payWindow.Draw();
            shippingWindow.Draw();

            Console.WriteLine("Choose payment option");
            var paymentOpt = Console.ReadKey(true);
            switch(paymentOpt.Key)
            {
                case ConsoleKey.D1:
                    order.PaymentOption = "Klarna";
                    totalPrice += 10;
                    break;
                case ConsoleKey.D2:
                    order.PaymentOption = "Creditcard";
                    break;
            }

            Console.WriteLine("Choose shippment");
            var shipment = Console.ReadKey(true);
            switch (shipment.Key)
            {
                case ConsoleKey.D1:
                    order.ShippmentOption = "Pick up";
                    totalPrice += 10;
                    break;
                case ConsoleKey.D2:
                    order.ShippmentOption = "Door Delivery";
                    totalPrice += 50;
                    break;
            }
        }

        public async Task Confirm(MyDbContext db)
        {
            List<string> confirm = new List<string>
            {
                "Confirm? Y/N"
            };
            var confirmWindow = new WindowManager("", 0, 20, confirm);
            confirmWindow.Draw();
            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.Y:
                    order.Time = DateTime.Now;

                    db.Order.Add(order);
                    db.SaveChanges();
                    TempCart.ClearTempCart();

                    var userLog = await Connection.GetUserLogsAsync();

                    var customer = db.Customer
                        .Where(c => c.Id == order.CustomerId)
                        .FirstOrDefault();


                    await userLog.InsertOneAsync(new Log<User>
                    {
                        Time = DateTime.Now,
                        Action = "Customer Made a order",
                        User = new User { Customer = customer, Order = order }
                    });

                    
                    break;
                case ConsoleKey.N:
                    
                    break;
            }
        }

        public void EnterInfo()
        {
            var guestCustomer = new Models.Customer();

            var fields = new Dictionary<string, Action<string>>
                {
                    { "Name", value => guestCustomer.Name = value },
                    { "Gender", value => guestCustomer.Gender = value },
                    { "Email", value => guestCustomer.Email = value },
                    { "PhoneNumber", value => guestCustomer.PhoneNumber = value },
                    { "Address", value => guestCustomer.Address = value },
                    { "City", value => guestCustomer.City = value },
                    { "Region", value => guestCustomer.Region = value },
                    { "PostalCode", value => guestCustomer.PostalCode = value }
                };

            foreach (var field in fields)
            {
                Console.Write($"Enter new {field.Key}: ");
                string input = Console.ReadLine();
                field.Value(input);
            }
            using (var db = new MyDbContext())
            {
                db.Customer.Add(guestCustomer);
                db.SaveChanges();
                Account.UpdateStatus(guestCustomer.Id, false);
            }
        }
    }
}

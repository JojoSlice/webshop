using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webshop.Helpers;
using webshop.Models;

namespace webshop
{
    internal class CustomerPage
    {
        public async Task Run(int id)
        {
            do
            {
                Console.Clear();
                using (var db = new MyDbContext())
                {
                    var customer = db.Customer
                        .Where(c => c.Id == id)
                        .FirstOrDefault();

                    List<string> customerInfo = new List<string>
                    {
                        $"Name:       {customer.Name}",
                        $"Gender:     {customer.Gender}",
                        $"Email:      {customer.Email}",
                        $"Phone:      {customer.PhoneNumber}",
                        $"Address:    {customer.Address}",
                        $"City:       {customer.City}",
                        $"Region:     {customer.Region}",
                        $"Postalcode: {customer.PostalCode}",
                    };
                    var infoWindow = new WindowManager("", 50, 0, customerInfo);
                    infoWindow.Draw();

                    var orders = db.Order
                        .Include(o => o.Garments)
                        .Where(c => c.CustomerId == customer.Id)
                        .ToList();

                    var orderInfo = new List<string>();
                    foreach (var order in orders)
                    {
                        orderInfo.AddRange(new List<string>
                        {
                            $"Order nr: {order.Id}",
                            $"Date: {order.Time}",
                            "___________________________"
                        });
                    }
                    var orderBox = new WindowManager("Orders", 0, 0, orderInfo);
                    orderBox.Draw();
                    var menu = new List<string>
                    {
                        "Press 'C' to Change information",
                        "Press 'O' to view Order specifics",
                        "Press 'Esc' to return"
                    };
                    var menuBox = new WindowManager("", 60, 30, menu);
                    menuBox.Draw();

                    var key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.C:
                            var changeInfo = new ChangeCustomerInfo();
                            await changeInfo.Run(id);
                            break;
                        case ConsoleKey.O:
                            ViewOrder(orders);
                            break;
                        case ConsoleKey.Escape:
                            return;
                    }
                }
            }while (true);
        }
        public void ViewOrder(List<Order> orders)
        {

            Console.WriteLine("Select order to view by order nr");

            if (int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.Clear();

                var specificOrder = orders.Where(o => o.Id == orderId).FirstOrDefault();

                if (specificOrder == null)
                {
                    Console.WriteLine($"Order with nr {orderId} does not exist");
                }

                var orderInfo = new List<string>
                {
                $"Payment: {specificOrder.PaymentOption}",
                $"Shipment: {specificOrder.ShippmentOption}",
                "____________________________________________"
                };


                foreach (var garment in specificOrder.Garments)
                {
                    string info = $"{garment.Name.PadRight(25)} Price: {garment.Price * garment.SaleTT}";
                    orderInfo.Add(info);
                }

                var orderWindow = new WindowManager("Order", 0, 0, orderInfo);
                orderWindow.Draw();
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("Enter a valid nummber");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webshop.Helpers;

namespace webshop
{
    internal class ViewCart
    {
        public List<Models.Garment> products = TempCart.GetTempCart().OfType<Models.Garment>().ToList();
        public List<string> View()
        {
            var list = new List<string>();
            float totalprice = 0;
            
            foreach (var product in products)
            {
                string text = $"{product.Name.PadRight(25)} Price: {product.Price * product.SaleTT}";
                list.Add(text);
                totalprice += (product.Price * product.SaleTT);
            }

            return list;
        }

        public void ManageCart()
        {
            do
            { 
                var cart = View();
                var cartWindow = new WindowManager("Cart", 0, 0, cart);

                var menuList = new List<string>
                { "Press 'R' to remove product from cart",
                  "Press 'C' to contiue to Check out",
                 "Press 'Esc' to return" };
                var menuWindow = new WindowManager("Menu", 30, 0, menuList);
                menuWindow.Draw();

                var key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.R:
                        RemoveFromCart();
                        break;
                    case ConsoleKey.C:
                        var checkOut = new CheckOut();
                        checkOut.Run();
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }while(true);
        }

        public void RemoveFromCart()
        {
            foreach (var product in products)
            {
                Console.WriteLine($"Id {product.Id}: {product.Name.PadRight(25)} {product.Price * product.SaleTT}");
            }
            Console.WriteLine("Write the id of the product you want to remove.");

            if ( int.TryParse( Console.ReadLine(), out var id) )
            {
                TempCart.RemoveFromTempCart(id);
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }
    }
}

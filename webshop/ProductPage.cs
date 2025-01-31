using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webshop.Helpers;
using webshop.Models;

namespace webshop
{
    internal class ProductPage
    {
        public void DisplayProduct(Models.Garment garment)
        {
            var productName = garment.Name;
            List<Models.Garment> sameProduct;
            using (var db = new MyDbContext())
            {
                sameProduct = db.Garment.Where(g => g.Name == productName).ToList();

            }

            var sizes = new List<string>();
            foreach (var p in sameProduct)
            {
                sizes.Add(p.Size.ToString());
            }
            var sizeString = string.Join(", ", sizes);

            var product = new List<string> { $"Price: {Math.Round(garment.Price * garment.SaleTT, 2)}",
            $"Aviable sizes: {sizeString}",
            $"Gender: {garment.Gender}",
            $"Color: {garment.Color}",
            $"{garment.Description}"};

            var windowGarment = new WindowManager(productName, 0, 0, product);
            windowGarment.Draw();

            var menu = new List<string> 
            { 
                "Press 'B' to Buy",
                "Press 'Esc' to Return to front page"
            };
            var windowMenu = new WindowManager("", 0, 6, menu);
            windowMenu.Draw();

            var windowSizes = new WindowManager("Sizes", 0, 12, sizes);

            var move = new List<string>
            {
                "Press 1 to continue to Cart",
                "Press 2 to keep shopping"
            };
            var windowMove = new WindowManager("", 0, 18, move);

            var key = Console.ReadKey(true);

            switch(key.Key)
            {
                case ConsoleKey.B:
                    windowSizes.Draw();
                    
                    Console.WriteLine("Pick available size");
                    bool pickSize = true;
                    do
                    {

                        if (int.TryParse(Console.ReadLine(), out int sizePick))
                        {
                            var buyProduct = sameProduct.Where(p => p.Size == sizePick).FirstOrDefault();
                            TempCart.AddTempCart(buyProduct);
                            pickSize = false;
                        }
                        else
                        {
                            Console.WriteLine("Enter a valid size");
                        }
                    } while (pickSize);




                    Console.Clear();
                    windowMove.Draw();

                    var key2 = Console.ReadKey(true);
                    if (key2.Key == ConsoleKey.D1)
                    {
                        Console.Clear();
                        var checkOut = new CheckOut();
                        checkOut.Run();
                    }
                    else { Console.Clear(); }
                    break;
            }
        }
    }
}

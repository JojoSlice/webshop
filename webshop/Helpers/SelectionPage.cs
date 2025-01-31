using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop.Helpers
{
    internal class SelectionPage
    {
        public void Selection(List<Models.Garment> garmentList)
        {
            var selectedList = garmentList
                .GroupBy(p => p.Name)
                .Select(group => group.First())
                .Select(p => $"{p.Name.PadRight(25)} Price: {Math.Round(p.Price, 2)} \n {p.Description}")
                .ToList();


            string[] menuItems = selectedList.ToArray();
            int selectedIndex = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.ResetColor();
                    }

                    Console.WriteLine(menuItems[i]);
                }

                Console.ResetColor();

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                key = keyInfo.Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    if (selectedIndex < 0)
                    {
                        selectedIndex = menuItems.Length - 1;
                    }
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selectedIndex++;
                    if (selectedIndex >= menuItems.Length)
                    {
                        selectedIndex = 0;
                    }
                }
                else if (key == ConsoleKey.Escape)
                {
                    return;
                }

            } while (key != ConsoleKey.Enter);

            var selected = menuItems[selectedIndex];

            string[] split = selected.Split(new[] { "Price" }, StringSplitOptions.RemoveEmptyEntries);
            string result = split[0].Trim(' ');

            var selectedGrament = garmentList.Where(g => g.Name == result).FirstOrDefault();

            Console.Clear();
            var productPage = new ProductPage();
            productPage.DisplayProduct(selectedGrament);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webshop.Models;

namespace webshop.Helpers
{
    internal class Categories
    {
        public async Task Run()
        {
            Console.Clear();
            try
            {
                using (var db = new MyDbContext())
                {
                    var garmentsByCategories = await db.Garment
                        .Include(g => g.Category)
                        .GroupBy(g => g.Category.Name)
                        .ToListAsync();
                    if (garmentsByCategories == null)
                    {
                        throw new Exception("Products could not be found");
                    }

                    int index = 1;
                    var keyMapping = new Dictionary<int, string>();
                    var boxText = new List<string>();

                    foreach (var group in garmentsByCategories)
                    {
                        boxText.Add($"{index}: {group.Key}");
                        keyMapping[index] = group.Key;
                        index++;
                    }

                    var window = new WindowManager("Categories", 0, 0, boxText);
                    window.Draw();

                    Console.WriteLine("Choose category by index");

                    var keyInfo = Console.ReadKey(true);
                    int key;

                    if (int.TryParse(keyInfo.KeyChar.ToString(), out key))
                    {
                        var selectedCategory = keyMapping[key];
                        var selectedGroup = garmentsByCategories
                            .FirstOrDefault(g => g.Key == selectedCategory)
                            .ToList();

                        var selection = new Helpers.SelectionPage();
                        selection.Selection(selectedGroup);
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

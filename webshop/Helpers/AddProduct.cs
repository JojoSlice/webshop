using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webshop.Models;

namespace webshop.Helpers
{
    internal class AddProduct
    {
        public async Task Run()
        {
            using var db = new MyDbContext();

            Models.Garment newGarment = null;

            Console.WriteLine("Do you wish to add'1' och change'2' a product?");
            var change = Console.ReadKey(true);
            if (change.Key == ConsoleKey.D1)
            {
                newGarment = new Models.Garment();
            }
            else
            {
                var dapper = new Dapper();
                try
                {
                    var garments = dapper.GetGraments();
                    if (garments == null)
                    {
                        throw new Exception("Problems with Dapper.GetGarments");
                    }

                    foreach (var garment in garments)
                    {
                        Console.WriteLine($"Id {garment.Id}: {garment.Name.PadRight(25)} Size: {garment.Size}");
                    }
                    Console.WriteLine("Enter id of product to change");

                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        newGarment = db.Garment.Where(g => g.Id == id).FirstOrDefault();
                        if (newGarment == null)
                        {
                            throw new Exception("Product could not be found");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            var fields = new Dictionary<string, Action<string>>
            {
                { "CategoryId", value => newGarment.CategoryId = int.Parse(value) },
                { "Name", value => newGarment.Name = value },
                { "Size", value => newGarment.Size = int.Parse(value) },
                { "Color", value => newGarment.Color = value },
                { "Gender", value => newGarment.Gender = value },
                { "Description", value => newGarment.Description = value },
                { "Price", value => newGarment.Price = float.Parse(value) },
                { "SaleTT", value => newGarment.SaleTT = float.Parse(value) },
                { "Stock", value => newGarment.Stock = int.Parse(value) },
                { "SupplierId", value => newGarment.SupplierId = int.Parse(value) }
            };

            var categories = db.Category
                .GroupBy (c => c.Name)
                .SelectMany(g => g.Select(c => new { Name = g.Key, Id = c.Id }))
                .ToList();

            foreach (var category in categories)
            {
                Console.WriteLine($"Id {category.Id}: {category.Name}");
            }

            foreach (var field in fields)
            {
                Console.WriteLine($"Enter: {field.Key}");
                string input = Console.ReadLine();
                if (input != "")
                {
                    field.Value(input);
                }
            }

            try
            {

                if (change.Key == ConsoleKey.D1)
                {
                    db.Garment.Add(newGarment);
                }
                else
                {
                    db.Garment.Update(newGarment);
                }

                await db.SaveChangesAsync();

                var admin = db.Admin
                    .Where(a => a.Id == Account.LoggedinID)
                    .FirstOrDefault();

                var adminLog = await Connection.GetAdminLogsAsync();

                await adminLog.InsertOneAsync(new Log<AdminUser>
                {
                    Time = DateTime.Now,
                    Action = "Admin added a product",
                    User = new AdminUser { Admin = admin, Garment = newGarment }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            } 
        }
    }
}

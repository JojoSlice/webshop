using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webshop.Models;

namespace webshop.Helpers
{
    internal class ChangeCustomerInfo
    {
        public async Task Run(int id)
        {
            Console.Clear();
            try
            {
                using (var db = new MyDbContext())
                {
                    var customer = await db.Customer
                        .Where(c => c.Id == id)
                        .FirstOrDefaultAsync();

                    var fields = new Dictionary<string, Action<string>>
                {
                    { "Name", value => customer.Name = value },
                    { "Gender", value => customer.Gender = value },
                    { "Email", value => customer.Email = value },
                    { "PhoneNumber", value => customer.PhoneNumber = value },
                    { "Address", value => customer.Address = value },
                    { "City", value => customer.City = value },
                    { "Region", value => customer.Region = value },
                    { "PostalCode", value => customer.PostalCode = value }
                };

                    foreach (var field in fields)
                    {
                        Console.Write($"Enter new {field.Key}: ");
                        string input = Console.ReadLine();
                        if (input != "")
                        {
                            field.Value(input);
                        }
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

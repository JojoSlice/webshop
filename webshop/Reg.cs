using Azure.Identity;
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
    internal class Reg
    {
        public async Task ReggIn()
        {
            var passHash = new Helpers.Password();
            Console.Clear();
            Console.WriteLine("Press 1 for Admin and 2 for Customer");
            var choice = Console.ReadKey(true);

            using (var db = new MyDbContext())
            {
                string? username;

                if (choice.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    List<string> text = new List<string> 
                    { "Provide following information:",
                      "Username",
                      "Password",
                      "Name"};
                    var regBox = new Helpers.WindowManager("", 0, 0, text);
                    regBox.Draw();

                    do
                    {
                        Console.Write("Username: ");
                        username = Console.ReadLine();

                        var admin = db.Admin
                            .FirstOrDefault(a => a.UserName == username);
                            
                        if (admin != null)
                        {
                            Console.WriteLine("Username is taken!");
                            username = null;
                        }

                    }while (username == null);

                    Console.WriteLine();
                    Console.Write("Password: ");
                    string password = passHash.ReadPassword();
                    string passwordHash = passHash.HashPassword(password);

                    Console.WriteLine();
                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    var newAdmin = new Models.Admin { Name = name, UserName = username, Password = passwordHash};

                    db.Admin.Add(newAdmin);
                    await db.SaveChangesAsync();

                    var adminLog = await Connection.GetAdminLogsAsync();
                        
                        await adminLog.InsertOneAsync(new Log<AdminUser>
                        {
                            Time = DateTime.Now,
                            Action = "New admin registration",
                            User = new AdminUser { Admin = newAdmin }
                        });

                    Account.UpdateStatus(newAdmin.Id, true);
                    return;
                }
                else if (choice.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                    List<string> text = new List<string> 
                    { "Provide following information:",
                      "Username",
                      "Password",
                      "Name",
                      "Gender",
                      "Email",
                      "Phonenumber",
                      "Address",
                      "City",
                      "Region",
                      "Postal code"};
                    var regBox = new Helpers.WindowManager("", 0, 0, text);
                    regBox.Draw();
                    var newCustomer = new Models.Customer(); 
                    do
                    {
                        Console.Write("Username: ");
                        username = Console.ReadLine();

                        var customer = await db.Customer
                            .FirstOrDefaultAsync(a => a.UserName == username);

                        if (customer != null)
                        {
                            Console.WriteLine("Username is taken!");
                            username = null;
                        }
                    }while (username == null);

                    newCustomer.UserName = username;
                    
                    Console.WriteLine();
                    Console.Write("Password: ");
                    
                    string password = passHash.ReadPassword();
                    string passwordHash = passHash.HashPassword(password);
                    newCustomer.Password = passwordHash;

                    var fields = new Dictionary<string, Action<string>>
                    {
                        { "Name", value => newCustomer.Name = value },
                        { "Gender", value => newCustomer.Gender = value },
                        { "Email", value => newCustomer.Email = value },
                        { "PhoneNumber", value => newCustomer.PhoneNumber = value },
                        { "Address", value => newCustomer.Address = value },
                        { "City", value => newCustomer.City = value },
                        { "Region", value => newCustomer.Region = value },
                        { "PostalCode", value => newCustomer.PostalCode = value }
                    };

                    foreach (var field in fields)
                    {
                        Console.Write($"Enter new {field.Key}: ");
                        string input = Console.ReadLine();
                        field.Value(input);
                    }

                    db.Customer.Add(newCustomer);
                    await db.SaveChangesAsync();

                    var userLog = await Connection.GetUserLogsAsync();

                        await userLog.InsertOneAsync(new Log<User>
                        {
                            Time = DateTime.Now,
                            Action = "new Customer registration",
                            User = new User { Customer = newCustomer }
                        });

                    Account.UpdateStatus(newCustomer.Id, false);
                }
            }

        }
    }
}

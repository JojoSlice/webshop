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
    internal class LogIn
    {
        public async Task Log()
        {
            var passHash = new Helpers.Password();
            Console.WriteLine("Username: ");
            var username = Console.ReadLine();

            Console.WriteLine("Password: ");
            string password = passHash.ReadPassword();

            try
            {
                using (var db = new MyDbContext())
                {

                    string hashedPassword = passHash.HashPassword(password);

                    var customer = await db.Customer
                        .FirstOrDefaultAsync(x => x.UserName == username && x.Password == hashedPassword);

                    if (customer != null)
                    {
                        Console.WriteLine($"Welcome, {customer.Name}!");
                        Account.UpdateStatus(customer.Id, false);
                        
                        var userLog = await Connection.GetUserLogsAsync();

                        await userLog.InsertOneAsync(new Log<User>
                        {
                            Time = DateTime.Now,
                            Action = "Customer logged in",
                            User = new User { Customer = customer }
                        });

                        return;
                    }

                    var admin = await db.Admin
                        .FirstOrDefaultAsync(x => x.UserName == username && x.Password == hashedPassword);

                    if (admin != null)
                    {
                        Console.WriteLine($"Welcome Admin, {admin.UserName}!");
                        Account.UpdateStatus(admin.Id, true);

                        var adminLog = await Connection.GetAdminLogsAsync();
                        
                        await adminLog.InsertOneAsync(new Log<AdminUser>
                        {
                            Time = DateTime.Now,
                            Action = "Admin logged in",
                            User = new AdminUser { Admin = admin }
                        });


                        return;
                    }

                    Console.WriteLine("User not found or password didn't match!");
                }
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Thread.Sleep(2000);
            Console.Clear();
        }
    }
}

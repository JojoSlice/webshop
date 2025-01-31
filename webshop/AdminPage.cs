using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webshop.Helpers;
using webshop.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace webshop
{
    enum Menu
    {
        Rekomended = 1,
        Change = 2,
        Customer_Info = 3,
        Statistiks = 4,
        Stock = 5,
        Exit = 6,
    }
    internal class AdminPage
    {
        public async Task Run()
        {
            do 
            {
                Console.Clear();
                var menu = new List<string>
                {
                    $"{(int)Menu.Rekomended}: Change rekomended products",
                    $"{(int)Menu.Change}: Add, Change or Remove products",
                    $"{(int)Menu.Customer_Info}: View customer info",
                    $"{(int)Menu.Statistiks}: View statistiks",
                    $"{(int)Menu.Stock}: Order more stock",
                    $"{(int)Menu.Exit}: to Exit"
                };
                var menuBox = new WindowManager("Menu", 0, 0, menu);
                menuBox.Draw();

                using (var db = new MyDbContext())
                {
                    var lowStockList = new List<string>();
                    var lowStock = db.Garment.Where(g => g.Stock < 10).ToList();
                    foreach(var g in lowStock)
                    {
                        lowStockList.Add($"Id {g.Id}: {g.Name.PadRight(25)} Stock: {g.Stock}");
                    }
                    var stockBox = new WindowManager("Low in stock", 50, 0, lowStockList);
                    if (lowStockList.Count > 0)
                    {
                        stockBox.Draw();
                    }
                }

                if(int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int nr))
                switch ((Menu)nr)
                {
                    case Menu.Rekomended:
                        Console.Clear();
                        var rekomended = new Rekomended();
                        rekomended.SetShow();
                        break;
                    case Menu.Change:
                        await AddRemove();
                        break;
                    case Menu.Customer_Info:
                        await ChangeCustomer();
                        break;
                    case Menu.Statistiks:
                        Console.Clear();
                        var stats = new Statistics();
                        await stats.Run();
                        break;
                    case Menu.Stock:
                        Console.WriteLine("Enter id of product");

                        if (int.TryParse(Console.ReadLine(), out nr))
                            {
                                await AddStock(nr);
                            }
                        else
                            {
                                Console.WriteLine("Enter a valid number");
                            }
                        break;
                    case Menu.Exit:
                        return;
                }

            }while (true);
        }

        public async Task AddStock(int id)
        {
            try
            {
                using var db = new MyDbContext();

                var garment = await db.Garment.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (garment == null)
                {
                    throw new Exception("Product was not found");
                }
                var supplier = await db.Supplier.Where(s => s.Id == garment.SupplierId).FirstOrDefaultAsync();
                if (supplier == null)
                {
                    throw new Exception("Supplier not found");
                }

                Console.WriteLine("How many have you ordered?");
                int input;

                if (int.TryParse(Console.ReadLine(), out input))
                {
                    garment.Stock += input;
                    Console.WriteLine($"The product {garment.Name} has been restocked with {input} pieces from {supplier.Name}");
                }
                else
                {
                    Console.WriteLine("Enter a valid nummber");
                }

                db.SaveChanges();
                Console.Clear();

                var admin = await db.Admin
                                .Where(a => a.Id == Account.LoggedinID)
                                .FirstOrDefaultAsync();
                try
                {

                    var adminLog = await Connection.GetAdminLogsAsync();

                    await adminLog.InsertOneAsync(new Log<AdminUser>
                    {
                        Time = DateTime.Now,
                        Action = "Admin made a restock",
                        User = new AdminUser { Admin = admin, Garment = garment }
                    });
                }
                catch (Exception logEx)
                {
                    Console.WriteLine(logEx.Message);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task ChangeCustomer()
        {
            var customerList = new List<string>();
            Console.Clear();
            try
            {
                using (var db = new MyDbContext())
                {
                    var customers = db.Customer.ToList();

                    if(customers == null)
                    {
                        throw new Exception("Customers could not be found in database");
                    }

                    int[] right = new int[] { 0, 30, 60 };
                    int size = customers.Count / 3;
                    int remain = customers.Count % 3;

                    for (var i = 0; i < 3; i++)
                    {
                        var splitList = customers.Skip(i * size).Take(size).ToList();

                        if (i == 2 && remain > 0)
                        {
                            splitList.AddRange(customers.Skip(3 * size));
                        }

                        foreach (var c in splitList)
                        {
                            string text = $"Id {c.Id}: {c.Name.PadRight(15)}";
                            customerList.Add(text);
                        }
                        var window = new WindowManager("", right[i], 0, customerList);
                        window.Draw();
                        customerList.Clear();
                    }

                    Console.WriteLine("Enter id of customer you want to view.");
                    int id;

                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.Clear();
                        var customerPage = new CustomerPage();
                        await customerPage.Run(id);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task AddRemove()
        {
            Console.WriteLine("1: to Add or Change, \n 2: to Remove");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {


                if (choice == 1)
                {
                    var add = new AddProduct();
                    await add.Run();

                }
                else if (choice == 2)
                {
                    Console.Clear();
                    var dapper = new Helpers.Dapper();
                    var garments = dapper.GetGraments();
                    var listGarments = new List<string>();
                    foreach (var g in garments)
                    {
                        string text = $"Id: {g.Id} {g.Name.PadRight(30)} Size: {g.Size}";
                        listGarments.Add(text);
                    }
                    var window = new WindowManager("", 0, 0, listGarments);
                    window.Draw();

                    Console.WriteLine("Enter id of product you want to remove.");
                    var remove = Console.ReadLine();

                    if (int.TryParse(remove, out var id))
                    {
                        try
                        {
                            var product = garments
                                .Where(g => g.Id == id)
                                .FirstOrDefault();
                            if (product == null)
                            {
                                throw new Exception("Product could not be found");
                            }

                            dapper.RemoveGarment(id);


                            using (var db = new MyDbContext())
                            {
                                var admin = await db.Admin
                                    .Where(a => a.Id == Account.LoggedinID)
                                    .FirstOrDefaultAsync();
                                try
                                {

                                    var adminLog = await Connection.GetAdminLogsAsync();

                                    await adminLog.InsertOneAsync(new Log<AdminUser>
                                    {
                                        Time = DateTime.Now,
                                        Action = "Admin removed a product",
                                        User = new AdminUser { Admin = admin, Garment = product }
                                    });
                                }
                                catch (Exception logEx)
                                {
                                    Console.WriteLine(logEx.Message);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid entry");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice");
                }
            }
            else
            {
                Console.WriteLine("Invalid entry");
            }
        }
    }
}

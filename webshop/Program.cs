using Bogus;
using Microsoft.EntityFrameworkCore.Diagnostics;
using webshop.Models;

namespace webshop
{
    internal class Program
    {
        //Ursäkta mina konstiga stavningar och ibland litte udda variabelnamn 
        static async Task Main(string[] args)
        {
            var Start = new StartPage();

            while (true)
            {
                Console.Clear();
                await Start.StartWindow();
            }


            //var propa = new Program();
            //propa.SaveCustomersToDatabase();
            //Console.WriteLine("Users Made");
            //propa.SaveOrdersToDatabase();
            //Console.WriteLine("Orders made");
            //Console.ReadLine();

        }

        //public void SaveCustomersToDatabase()
        //{
        //    using var db = new MyDbContext();
        //    var faker = new Helpers.Faker();

        //    var customers = faker.GenerateCustomers(50);

        //    db.Customer.AddRange(customers);
        //    db.SaveChanges();
        //}

        //public void SaveOrdersToDatabase()
        //{
        //    using var db = new MyDbContext();
        //    var faker = new Helpers.Faker();
        //    var customers = db.Customer.ToList();
        //    var garments = db.Garment.ToList();

        //    var orders = faker.GenerateOrders(100, customers, garments);

        //    db.Order.AddRange(orders);
        //    db.SaveChanges();
        //}
    }
}

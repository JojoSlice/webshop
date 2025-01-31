using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using webshop.Migrations;
using webshop.Models;

namespace webshop
{
    internal class Statistics
    {
        public async Task Run()
        {
            Spectre.Console.Color[] colors = { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Silver };

            using (var db = new MyDbContext())
            {
                var orders = await db.Order.Include(o => o.Garments).ToListAsync();
                await BarChartGender(db, colors);
                await BarChartRegion(db, colors, orders);
                BarChartCaterories(db, colors);

                Console.WriteLine("Press Esc to exit");
                Console.ReadKey(true);
                Console.Clear();
            }
        }

        public void BarChartCaterories(MyDbContext db, Color[] colors)
        {
            var barChartCategories = new BarChart()
                .Width(100)
                .Label("[red bold underline]Most bought by category[/]")
                .CenterLabel();
            var allGarments = new List<Garment>();

            var orders = db.Order
                .Include(o => o.Garments)
                .ThenInclude(g => g.Category)
                .ToList();
            
            foreach (var order in orders)
            {
                var garments = order.Garments
                    .ToList();
                allGarments.AddRange(garments);
            }
            
            var garmentsGrouped = allGarments.GroupBy(g => g.Category.Name).ToList();

            int index = 0;
            foreach (var group in garmentsGrouped)
            {
                barChartCategories.AddItem(group.Key, group.Count(), colors[index]);
                index++;
            }

            if (garmentsGrouped.Count() > 0)
            {
                AnsiConsole.Write(barChartCategories);
            }
            else
            { return; }
        }
        public async Task BarChartRegion(MyDbContext db, Color[] colors, List<Order> orders)
        {
            var customers = await db.Customer.ToListAsync();

            var regions = customers
                .GroupBy(c => c.Region)
                .ToList();

            var barChartRegion = new BarChart()
                .Width(100)
                .Label($"[red bold underline]Orders by Region[/]")
                .CenterLabel();

            var index = 0;  
            foreach (var region in regions)
            {
                if (index == colors.Length)
                { index = 0; }

                var ordersInRegion = new List<Order>();

                foreach (var customer in region)
                {
                    var customerOrder = orders
                        .Where(o => o.CustomerId == customer.Id)
                        .ToList();

                    ordersInRegion.AddRange(customerOrder);
                }

                var allGarments = new List<Garment>();

                foreach (var order in ordersInRegion)
                {
                    var garments = order.Garments.ToList();
                    allGarments.AddRange(garments);
                }


                if (allGarments.Count > 0)
                {
                    barChartRegion.AddItem(region.Key, allGarments.Count(), colors[index]);
                }
                
                index++;
            }
            AnsiConsole.Write(barChartRegion);
        }

        public async Task BarChartGender(MyDbContext db, Spectre.Console.Color[] colors)
        {
            var genderGroup = await db.Customer
                    .GroupBy(c => c.Gender)
                    .ToListAsync();

            foreach (var group in genderGroup)
            {
                var barCharGender = new BarChart()
                    .Width(100)
                    .Label($"[green bold underline]Most bought by {group.Key}[/]")
                    .CenterLabel();

                var allGarmentIds = new List<int>();
                allGarmentIds.Clear();

                foreach (var customer in group)
                {

                    var orders = await db.Order
                        .Where(c => c.CustomerId == customer.Id)
                        .ToListAsync();


                    foreach (var order in orders)
                    {
                        foreach (var id in order.Garments)
                        {
                            allGarmentIds.Add(id.Id);
                        }
                    }


                }
                var mostBoughtGarments = allGarmentIds
                    .GroupBy(g => g)
                    .Select(g => new
                    {
                        GarmentId = g.Key,
                        Count = g.Count()
                    })
                    .OrderByDescending(g => g.Count)
                    .Take(5)
                    .ToList();

                int index = 0;
                foreach (var g in mostBoughtGarments)
                {
                    var garment = await db.Garment.Where(n => n.Id == g.GarmentId).FirstOrDefaultAsync();

                    barCharGender.AddItem(garment.Name, g.Count, colors[index]);
                    index++;
                }

                if(mostBoughtGarments.Count > 0)
                {
                    AnsiConsole.Write(barCharGender);
                }
            }
        }
    }
}

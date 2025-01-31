using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using webshop.Models;

namespace webshop.Helpers
{
    internal class Search
    {
        public async Task SearchBarAsync()
        {
            Console.WriteLine("Search: ");
            string search = Console.ReadLine();

            Console.Clear();

            using (var db = new MyDbContext())
            {
                var results = await db.Garment
                    .Where(g => EF.Functions.Like(g.Name, $"%{search}%") || EF.Functions.Like(g.Description, $"%{search}%"))
                    .ToListAsync();

                results = results
                    .GroupBy(g => g.Name)
                    .SelectMany(group => group)
                    .ToList();


                var select = new SelectionPage();

                select.Selection(results); 
            }
        }
    }
}

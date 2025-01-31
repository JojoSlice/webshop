using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webshop.Migrations;
using webshop.Models;

namespace webshop.Helpers
{
    internal class Popular
    {
        public async Task<List<Models.Garment>> GetPopularAsync()
        {
            using (var db = new MyDbContext())
            {
                var popularProducts = await db.Order
                    .SelectMany(o => o.Garments)
                    .GroupBy(g => g.Id)
                    .OrderByDescending(group => group.Count())
                    .Take(5)
                    .Select(group => group.FirstOrDefault())
                    .ToListAsync();

                if (!popularProducts.Any())
                {
                    Random rnd = new Random();
                    popularProducts = await db.Garment
                        .Distinct()
                        .ToListAsync();

                    popularProducts = popularProducts
                        .OrderBy(_ => rnd.Next(db.Garment.Count()))
                        .Take(5)
                        .ToList();
                }
                return popularProducts;
            }
        }


    }
}

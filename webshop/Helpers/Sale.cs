using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webshop.Models;

namespace webshop.Helpers
{
    internal class Sale
    {
        public async Task<List<Models.Garment>> GetSaleAsync()
        {
            using (var db = new MyDbContext())
            {
                List<Models.Garment> productOnSale = await db.Garment
                    .Where(g => g.SaleTT < 1)
                    .ToListAsync();

                return productOnSale;
            }
        }
    }
}

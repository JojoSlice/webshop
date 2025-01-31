using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop.Models
{
    internal class MyDbContext : DbContext
    {
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Garment> Garment { get; set; }
        public DbSet<Order> Order {  get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasIndex(a => a.UserName).IsUnique();
            modelBuilder.Entity<Customer>().HasIndex(c => new { c.UserName, c.Email }).IsUnique();
            modelBuilder.Entity<Garment>().HasIndex(g => g.Id).IsUnique();
            modelBuilder.Entity<Order>().HasIndex(c => c.Id).IsUnique();
            modelBuilder.Entity<Supplier>().HasIndex(s => s.Id).IsUnique();
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:johannesdb.database.windows.net,1433;Initial Catalog=johannesdb;Persist Security Info=False;User ID=jojoSlice;Password=Skolan12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
//Server=.\\SQLExpress;Database=WebShop;Trusted_Connection=True; TrustServerCertificate=True;

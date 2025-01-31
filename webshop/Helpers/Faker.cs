using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webshop.Models;

namespace webshop.Helpers
{
    internal class Faker
    {
        public List<Customer> GenerateCustomers(int count)
        {
            var pass = new Password();

            var customerFaker = new Faker<Customer>()
                .RuleFor(c => c.Name, f => f.Name.FullName())
                .RuleFor(c => c.UserName, f => f.Internet.UserName())
                .RuleFor(c => c.Password, (f, c) => pass.HashPassword(f.Internet.Password()))
                .RuleFor(c => c.Gender, f => f.PickRandom("Male", "Female"))
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.StreetAddress())
                .RuleFor(c => c.City, f => f.Address.City())
                .RuleFor(c => c.Region, f => f.Address.State())
                .RuleFor(c => c.PostalCode, f => f.Address.ZipCode());

            return customerFaker.Generate(count);
        }

        public List<Order> GenerateOrders(int count, List<Customer> customers, List<Garment> garments)
        {
            var orderFaker = new Faker<Order>()
                .RuleFor(o => o.CustomerId, f => f.PickRandom(customers).Id) // Koppla ordern till en slumpmässig kund
                .RuleFor(o => o.Time, f => f.Date.Past(1)) // Slumpa en datum/tid för ordern, max 1 år gammal
                .RuleFor(o => o.PaymentOption, f => f.PickRandom("Klarna", "Creditcard")) // Slumpa betalningsalternativ
                .RuleFor(o => o.ShippmentOption, f => f.PickRandom("Pick up", "Door Delivery")) // Slumpa fraktalternativ
                .RuleFor(o => o.Garments, f =>
                    new List<Garment>(f.PickRandom(garments, f.Random.Int(1, 5)))); // Skapa en lista av slumpmässiga Garments

            return orderFaker.Generate(count); // Generera det angivna antalet ordrar
        }

    }
}

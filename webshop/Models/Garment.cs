using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop.Models
{
    internal class Garment
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [BsonIgnore]
        public virtual Category Category { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string Color { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public float SaleTT { get; set; }//Sales times table, multiply against price for sales price
        public int Stock { get; set; }
        public int SupplierId { get; set; }
        [BsonIgnore]
        public virtual Supplier Supplier { get; set; }
        public bool Rekomended { get; set; } = false;
        [BsonIgnore]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

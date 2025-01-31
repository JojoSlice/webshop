using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop.Models
{
    internal class Order
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public DateTime Time { get; set; }
        [BsonIgnore]
        public string PaymentOption { get; set; }
        [BsonIgnore]
        public string ShippmentOption { get; set; }
        [BsonIgnore]
        public virtual ICollection<Garment> Garments { get; set; } = new List<Garment>();
    }
}

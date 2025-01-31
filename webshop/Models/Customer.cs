using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop.Models
{
    internal class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? UserName { get; set; }

        [BsonIgnore]
        public string? Password { get; set; }
        [BsonIgnore]
        public string? Gender { get; set; }
        [BsonIgnore]
        public string Email { get; set; }
        [BsonIgnore]
        public string? PhoneNumber { get; set; }
        [BsonIgnore]
        public string Address { get; set; }
        [BsonIgnore]
        public string City { get; set; }
        [BsonIgnore]
        public string Region { get; set; }
        [BsonIgnore]
        public string PostalCode { get; set; }

        [BsonIgnore]
        public ICollection<Order> Orders { get; set; }

    }
}

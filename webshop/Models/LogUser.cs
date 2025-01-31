using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop.Models
{
    internal class Log<TUser>
    {
        public DateTime Time { get; set; }
        public string Action { get; set; }
        public TUser User { get; set; }
    }

    internal class User
    {
        public Customer Customer { get; set; }
        public Order? Order { get; set; }
    }

    internal class AdminUser
    {
        public Admin Admin { get; set; }
        public Garment? Garment { get; set; }
    }
}

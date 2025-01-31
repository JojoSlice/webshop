using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop.Models
{
    internal class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Garment> Graments { get; set; } = new List<Garment>();
    }
}

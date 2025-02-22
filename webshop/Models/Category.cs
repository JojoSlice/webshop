﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop.Models
{
    internal class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Garment> Garments { get; set; } = new List<Garment>();
    }
}

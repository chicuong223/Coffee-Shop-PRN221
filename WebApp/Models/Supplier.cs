using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Supplies = new HashSet<Supply>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Supply> Supplies { get; set; }
    }
}

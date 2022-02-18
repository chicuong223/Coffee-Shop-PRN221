using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class Table
    {
        public Table()
        {
            Bills = new HashSet<Bill>();
        }

        public int Id { get; set; }
        public int? MaxNumber { get; set; }
        public int? Status { get; set; }
        public bool? Available { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}

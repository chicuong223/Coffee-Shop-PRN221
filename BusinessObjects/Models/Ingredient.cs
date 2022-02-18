using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            Notifications = new HashSet<Notification>();
            Supplies = new HashSet<Supply>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Stock { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Supply> Supplies { get; set; }
    }
}

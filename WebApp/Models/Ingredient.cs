using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            NotificationDetails = new HashSet<NotificationDetail>();
            Supplies = new HashSet<Supply>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Stock { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<NotificationDetail> NotificationDetails { get; set; }
        public virtual ICollection<Supply> Supplies { get; set; }
    }
}

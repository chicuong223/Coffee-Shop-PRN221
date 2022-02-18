using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public int? IngredientId { get; set; }
        public bool? IsRead { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual staff SenderNavigation { get; set; }
    }
}

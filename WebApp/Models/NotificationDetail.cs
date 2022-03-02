using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp.Models
{
    public partial class NotificationDetail
    {
        public int NotificationId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Notification Notification { get; set; }
    }
}

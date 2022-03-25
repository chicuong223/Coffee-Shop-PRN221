using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataObject.Models
{
    public partial class Notification
    {
        public Notification()
        {
            NotificationDetails = new HashSet<NotificationDetail>();
        }

        public int Id { get; set; }
        public string Sender { get; set; }
        public bool IsRead { get; set; }
        public bool IsSent { get; set; }
        [Display(Name = "Time")]
        public DateTime? NotificationDate { get; set; }

        [Display(Name = "Sender")]
        public virtual Staff SenderNavigation { get; set; }
        public virtual ICollection<NotificationDetail> NotificationDetails { get; set; }
    }
}

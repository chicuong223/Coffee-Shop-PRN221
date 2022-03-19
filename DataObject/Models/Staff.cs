using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataObject.Models
{
    public partial class Staff
    {
        public Staff()
        {
            Bills = new HashSet<Bill>();
            Notifications = new HashSet<Notification>();
        }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "Username must be 3 to 32 character long")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Password must be 5 to 50 character long")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataObject.Models
{
    public partial class Voucher
    {
        public Voucher()
        {
            Bills = new HashSet<Bill>();
        }

        public string Id { get; set; }
        [Required(ErrorMessage = "Voucher name is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Discount rate is required!")]
        public double? Percentage { get; set; }
        [Required(ErrorMessage = "Expiration Date is required!")]
        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }
        [Required(ErrorMessage = "Usage times is required!")]
        public int? UsageCount { get; set; }

        [DisplayName("Active")]
        public bool Status { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}

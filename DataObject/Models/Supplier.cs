using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataObject.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Supplies = new HashSet<Supply>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Supplier Name is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Supplier Phone is required!")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }
        [DisplayName("Acitve")]
        public bool Status { get; set; }

        public virtual ICollection<Supply> Supplies { get; set; }
    }
}

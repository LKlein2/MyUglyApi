using System;
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Model
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Field {0} required")]
        [StringLength(100, ErrorMessage = "Field {0} must be between {2} and {1} characters", MinimumLength = 10)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field {0} required")]
        [StringLength(14, ErrorMessage = "Field {0} must be between {2} and {1} characters", MinimumLength = 11)]
        public string Document { get; set; }

        public int SupplierType { get; set; }

        public bool Active { get; set; }
    }
}

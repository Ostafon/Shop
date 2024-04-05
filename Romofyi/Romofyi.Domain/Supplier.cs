using System.ComponentModel.DataAnnotations;

namespace Romofyi.Domain
{
    public class Supplier
    {
        public int SupplierID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Supplier Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [StringLength(200)]
        [Display(Name = "Address")]
        public string Address { get; set; }
    }
}
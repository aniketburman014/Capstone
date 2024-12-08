using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarManufacturingIndustryManagement.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Customer Name is Required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Customer Name length should be between 5 to 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Customer Name should only contain alphabets and spaces.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Contact Details are Required")]
        [MaxLength(10, ErrorMessage = "Contact Details should not exceed 10 characters.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact Details must be exactly 10 digits.")]
        public string ContactDetails { get; set; }

        [MaxLength(500, ErrorMessage = "Purchase History should not exceed 500 characters.")]
        public string PurchaseHistory { get; set; }

        [Required(ErrorMessage = "Status is Required")]
        [MaxLength(50, ErrorMessage = "Status should not exceed 50 characters.")]
        public string Status { get; set; }
    }
}

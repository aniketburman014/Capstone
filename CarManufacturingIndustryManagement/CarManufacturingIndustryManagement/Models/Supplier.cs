using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarManufacturingIndustryManagement.Models
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Supplier Name is Required")]
        [MaxLength(100, ErrorMessage = "Supplier Name should not exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Contact Details are Required")]
        [MaxLength(10, ErrorMessage = "Contact Details should not exceed 10 characters.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact Details must be exactly 10 digits.")]
        public string ContactDetails { get; set; }

        [Required(ErrorMessage = "Material Type is Required")]
        [MaxLength(100, ErrorMessage = "Material Type should not exceed 100 characters.")]
        public string MaterialType { get; set; }

        [Required(ErrorMessage = "Delivery Time is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Delivery Time must be greater than 0 days.")]
        public int DeliveryTime { get; set; } // In days

        [Required(ErrorMessage = "Pricing is Required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Pricing must be greater than 0.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Pricing { get; set; }

        [Required(ErrorMessage = "Status is Required")]
        [MaxLength(50, ErrorMessage = "Status should not exceed 50 characters.")]
        public string Status { get; set; } 
    }
}

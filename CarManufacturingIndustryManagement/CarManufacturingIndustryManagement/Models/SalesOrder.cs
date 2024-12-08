using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarManufacturingIndustryManagement.Models
{
    public class SalesOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Customer ID is Required")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Car Model ID is Required")]
        [ForeignKey("Car")]
        public int CarModelId { get; set; }

        [Required(ErrorMessage = "Order Date is Required")]
        public DateTime OrderDate { get; set; }

        public DateTime? DeliveryDate { get; set; } 

        [Required(ErrorMessage = "Price is Required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Status is Required")]
        [MaxLength(50, ErrorMessage = "Status should not exceed 50 characters.")]
        public string Status { get; set; }
    }
}

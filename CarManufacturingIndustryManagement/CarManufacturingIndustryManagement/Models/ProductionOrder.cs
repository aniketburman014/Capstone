using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarManufacturingIndustryManagement.Models
{
    public class ProductionOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Car Model ID is Required")]
        [ForeignKey("CarModel")]
        public int CarModelId { get; set; }


        [Required(ErrorMessage = "Start Date is Required")]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; } 

        [Required(ErrorMessage = "Quantity is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Production Manager ID is Required")]
        [ForeignKey("User")]
        public int ProductionManagerId { get; set; }

        [Required(ErrorMessage = "Status is Required")]
        [MaxLength(50, ErrorMessage = "Status should not exceed 50 characters.")]
        public string Status { get; set; } 
    }
}

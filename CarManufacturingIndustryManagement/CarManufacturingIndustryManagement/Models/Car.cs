using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarManufacturingIndustryManagement.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModelId { get; set; }

        [Required(ErrorMessage = "Model Name is Required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Model Name length should be between 5 to 50 characters.")]
        public string ModelName { get; set; }

        [Required(ErrorMessage = "Engine Type is Required")]
        [MaxLength(50, ErrorMessage = "Engine Type should not exceed 50 characters.")]
        public string EngineType { get; set; }

        [Required(ErrorMessage = "Fuel Efficiency is Required")]
        [Range(0.01, float.MaxValue, ErrorMessage = "Fuel Efficiency must be greater than 0.")]
        public float FuelEfficiency { get; set; }

        [Required(ErrorMessage = "Design Features are Required")]
        [MaxLength(500, ErrorMessage = "Design Features should not exceed 500 characters.")]
        public string DesignFeatures { get; set; }

        [Required(ErrorMessage = "Production Cost is Required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Production Cost must be greater than 0.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductionCost { get; set; }

        [Required(ErrorMessage = "Status is Required")]
        [MaxLength(50, ErrorMessage = "Status should not exceed 50 characters.")]
        public string Status { get; set; }
    }
}

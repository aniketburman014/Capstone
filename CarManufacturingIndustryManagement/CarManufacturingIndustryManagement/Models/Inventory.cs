using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarManufacturingIndustryManagement.Models
{
    public class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryId { get; set; }

        [Required(ErrorMessage = "Component Name is Required")]
        [MaxLength(100, ErrorMessage = "Component Name should not exceed 100 characters.")]
        public string ComponentName { get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative integer.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Supplier ID is Required")]
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Stock Level is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock Level must be a non-negative integer.")]
        public int StockLevel { get; set; }

        [Required(ErrorMessage = "Reorder Threshold is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Reorder Threshold must be a non-negative integer.")]
        public int ReorderThreshold { get; set; }
    }
}

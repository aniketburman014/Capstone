using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarManufacturingIndustryManagement.Models
{
    public class QualityReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReportId { get; set; }

        [Required(ErrorMessage = "Car Model ID is Required")]
        [ForeignKey("CarModel")]
        public int CarModelId { get; set; }

        [Required(ErrorMessage = "Inspection Date is Required")]
        public DateTime InspectionDate { get; set; }

        [Required(ErrorMessage = "Inspector ID is Required")]
        [ForeignKey("User")]
        public int InspectorId { get; set; }

        [Required(ErrorMessage = "Test Results are Required")]
        [MaxLength(1000, ErrorMessage = "Test Results should not exceed 1000 characters.")]
        public string TestResults { get; set; }

        [MaxLength(1000, ErrorMessage = "Defects Found should not exceed 1000 characters.")]
        public string DefectsFound { get; set; }

        [Required(ErrorMessage = "Status is Required")]
        [MaxLength(50, ErrorMessage = "Status should not exceed 50 characters.")]
        public string Status { get; set; }
    }
}

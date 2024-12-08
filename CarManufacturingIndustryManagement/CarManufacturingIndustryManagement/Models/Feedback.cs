using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarManufacturingIndustryManagement.Models
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is Required")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}

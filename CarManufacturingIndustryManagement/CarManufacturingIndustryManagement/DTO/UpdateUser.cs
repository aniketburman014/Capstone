using System.ComponentModel.DataAnnotations;

namespace CarManufacturingIndustryManagement.DTO
{
    public class UpdateUser
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is Required")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Role is Required")]
        [MaxLength(50, ErrorMessage = "Role cannot exceed 50 characters.")]
        public string Role { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

    }
}

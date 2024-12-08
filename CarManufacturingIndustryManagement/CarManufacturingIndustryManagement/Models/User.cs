using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarManufacturingIndustryManagement.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is Required")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password  is Required")]
        [MaxLength(100, ErrorMessage = "Password  cannot exceed 100 characters.")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Role is Required")]
        [MaxLength(50, ErrorMessage = "Role cannot exceed 50 characters.")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Is Active status is Required")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Created Date is Required")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Updated Date is Required")]
        public DateTime UpdatedAt { get; set; }
    }
}

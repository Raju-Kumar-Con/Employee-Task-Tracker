using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTaskTracker.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name Required")]
        [MinLength(2, ErrorMessage = "Minimum 2 characters required")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters allowed")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name Required")]
        [MinLength(2, ErrorMessage = "Minimum 2 characters required")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters allowed")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number Required")]
        [RegularExpression(@"^[0-9]{10}$",
            ErrorMessage = "Phone must be exactly 10 digits")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Range(18, 60,
            ErrorMessage = "Age must be between 18 and 60")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Gender Required")]
        public string Gender { get; set; } = string.Empty;

        public string? Hobbies { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        public string? FavoriteColor { get; set; }

        [Range(1, 100,
            ErrorMessage = "Skill Range must be between 1 and 100")]
        public int SkillRange { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [MinLength(6,
            ErrorMessage = "Password must contain minimum 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        // For File Upload
        [NotMapped]
        public IFormFile? UploadFile { get; set; }

        // Store Uploaded File Name
        public string? FileName { get; set; }
    }
}
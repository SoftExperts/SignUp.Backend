using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class UserDto
    {
        [Required]
        [MaxLength(12, ErrorMessage = "Up to 12 characters allowed")]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(12, ErrorMessage = "Up to 12 characters allowed")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must contain at least 8 characters, including one uppercase letter, one lowercase letter, one digit, and one special character.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}

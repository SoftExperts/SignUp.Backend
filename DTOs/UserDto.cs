using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class UserDto
    {
        [Required]
        [MaxLength(12, ErrorMessage = "Upto 12 characters allowed")]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(12, ErrorMessage = "Upto 12 characters allowed")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}

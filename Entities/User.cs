﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class User : IdentityUser
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
    }
}
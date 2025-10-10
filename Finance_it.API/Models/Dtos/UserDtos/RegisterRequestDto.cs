using System.ComponentModel.DataAnnotations;
using Finance_it.API.Data.Entities;

namespace Finance_it.API.Models.Dtos.UserDtos
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = " Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; } 
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; } 
        public Role? Role { get; set; }
    }
}

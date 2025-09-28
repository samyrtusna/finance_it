using System.ComponentModel.DataAnnotations;

namespace Finance_it.API.Dtos.UserDtos
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; } 
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}

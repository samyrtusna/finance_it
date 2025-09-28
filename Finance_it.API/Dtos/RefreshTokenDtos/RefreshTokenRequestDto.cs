using System.ComponentModel.DataAnnotations;

namespace Finance_it.API.Dtos.RefreshTokenDtos
{
    public class RefreshTokenRequestDto
    {
        [Required(ErrorMessage = "Token is required.")]
        public string? Token { get; set; } 
        public int UserId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Finance_it.API.Models.Dtos.RefreshTokenDtos
{
    public class RefreshTokenRequestDto
    {
        [Required(ErrorMessage = "Token is required.")]
        public string Token { get; set; } = default!;
        public int UserId { get; set; }
    }
}

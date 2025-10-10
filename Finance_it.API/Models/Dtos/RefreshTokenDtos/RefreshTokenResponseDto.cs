using System.ComponentModel.DataAnnotations;

namespace Finance_it.API.Models.Dtos.RefreshTokenDtos
{
    public class RefreshTokenResponseDto
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } 
        [Required]
        public DateTime ExpiresAt { get; set; }
        [Required]
        public bool IsRevoked { get; set; } 
        public DateTime RevokedAt { get; set; }
    }
}

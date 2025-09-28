using System.ComponentModel.DataAnnotations;
using Finance_it.API.Models;

namespace Finance_it.API.Dtos.RefreshTokenDtos
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
        public DateTime? RevokedAt { get; set; }
    }
}

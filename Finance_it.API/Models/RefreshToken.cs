using System.ComponentModel.DataAnnotations;

namespace Finance_it.API.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Token is required.")]
        public string Token { get; set; } = null!;
        public int UserId { get; set; }
        public virtual User User { get; set; } 
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime ExpiresAt { get; set; }
        [Required]
        public bool IsRevoked { get; set; } = false;
        public DateTime? RevokedAt { get; set; }
    }
}

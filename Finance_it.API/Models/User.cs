using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Finance_it.API.Models
{
    [Index(nameof(Email), Name = "IX_Email_Unique", IsUnique =true)]
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage ="Name must be between 2 and 50 characters.")]
        public string Name { get; set; }=string.Empty;
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; } = null!;
        [Required]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Required(ErrorMessage ="Login date is required.")]
        public DateTime LastLogin {  get; set; }
        [Required(ErrorMessage = "User Role is required.")]
        [EnumDataType(typeof(Role), ErrorMessage = "Invalid User Role.")]
        public virtual Role Role { get; set; } = Role.User;
        public virtual ICollection<FinancialEntry> FinancialEntries { get; set; } = [];
        public virtual ICollection<FinancialScore> FinancialScores { get; set; } = [];
        public virtual ICollection<Recommendation> Recommendations { get; set; } = [];
        public virtual ICollection<Goal> Goals { get; set; } = [];
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    }
}

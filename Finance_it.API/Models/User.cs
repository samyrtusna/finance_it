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
        public string Email { get; set; }= string.Empty;
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; } = string.Empty;
        [Required]
        public DateTime CreateAt { get; set; } = DateTime.Now;
        [Required(ErrorMessage ="A Login date is required.")]
        public DateTime LastLogin {  get; set; }
        [Required(ErrorMessage ="A User Role is required.")]
        [EnumDataType(typeof(Role), ErrorMessage ="Invalid User Role.")]
        public Role Role { get; set; }
        public ICollection<FinancialEntry> FinancialEntries { get; set; } = [];
        public ICollection<FinancialScore> FinancialScores { get; set; } = [];
        public ICollection<Recommendation> Recommendations { get; set; } = [];
        public ICollection<Goal> Goals { get; set; } = [];
        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    }
}

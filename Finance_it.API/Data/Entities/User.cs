using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Finance_it.API.Data.Entities
{
    [Index(nameof(Email), Name = "IX_Email_Unique", IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
        [Required]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Required(ErrorMessage = "Login date is required.")]
        public DateTime LastLogin { get; set; }
        [Required(ErrorMessage = "User Role is required.")]
        [EnumDataType(typeof(Role), ErrorMessage = "Invalid User Role.")]
        public Role Role { get; set; } 
        public virtual ICollection<FinancialEntry> FinancialEntries { get; set; } = [];
        public virtual ICollection<Recommendation> Recommendations { get; set; } = [];
        public virtual ICollection<Goal> Goals { get; set; } = [];
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = [];
        public virtual ICollection<Category> Categories { get; set; } = [];
        public virtual ICollection<WeeklyAggregate> WeeklyAgregates { get; set; } = [];
        public virtual ICollection<MonthlyAggregate> MonthlyAgregates { get; set; } = [];
        public virtual ICollection<YearlyAggregate> YearlyAgregates { get; set; } = [];
    }
}

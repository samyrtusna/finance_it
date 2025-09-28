using System.ComponentModel.DataAnnotations;

namespace Finance_it.API.Models
{
    public class FinancialScore
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required(ErrorMessage ="Score is required.")]
        [Range(0.00, 100.00, ErrorMessage ="Score value must be between 0 and 100.")]
        public decimal Score { get; set; }
        [Required(ErrorMessage ="Calculation date is required.")]
        public DateTime CalculatedAt { get; set; }
        public virtual ICollection<ScoreDetail> ScoreDetails { get; set; } = []; 
    }
}

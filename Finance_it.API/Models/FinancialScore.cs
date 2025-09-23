using System.ComponentModel.DataAnnotations;

namespace Finance_it.API.Models
{
    public class FinancialScore
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }= new();
        [Required(ErrorMessage ="A Score is required.")]
        [Range(0.00, 100.00, ErrorMessage ="Score value must be between 0 and 100.")]
        public decimal Score { get; set; }
        [Required(ErrorMessage ="A Calculation date is required.")]
        public DateTime CalculatedAt { get; set; }
        public ICollection<ScoreDetail> ScoreDetails { get; set; } = []; 
    }
}

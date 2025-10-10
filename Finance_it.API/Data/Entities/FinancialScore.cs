using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance_it.API.Data.Entities
{
    public class FinancialScore
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required(ErrorMessage ="Score is required.")]
        [Range(0.00, 100.00, ErrorMessage ="Score value must be between 0 and 100.")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Score { get; set; }
        [Required(ErrorMessage ="Calculation date is required.")]
        public DateTime CalculatedAt { get; set; }
        public virtual ICollection<ScoreDetail> ScoreDetails { get; set; } = []; 
    }
}

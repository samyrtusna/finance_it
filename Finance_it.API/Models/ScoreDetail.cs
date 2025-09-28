using System.ComponentModel.DataAnnotations;

namespace Finance_it.API.Models
{
    public class ScoreDetail
    {
        public int Id { get; set; }
        public int FinancialScoreId { get; set; }
        public virtual FinancialScore FinancialScore { get; set; } = new();
        [Required(ErrorMessage ="Criterion is required.")]
        public string Criterion { get; set; }= null!;
        [Required(ErrorMessage ="Criterion Value is required.")]
        public decimal CriterionValue { get; set; } 
    }
}

using System.ComponentModel.DataAnnotations;

namespace Finance_it.API.Models
{
    public class ScoreDetail
    {
        public int Id { get; set; }
        public int FinancialScoreId { get; set; }
        public FinancialScore FinancialScore { get; set; } = new();
        [Required(ErrorMessage ="A Criterion is required.")]
        public string Criterion { get; set; }= string.Empty;
        [Required(ErrorMessage ="A Criterion Value is required.")]
        public decimal CriterionValue { get; set; } 
    }
}

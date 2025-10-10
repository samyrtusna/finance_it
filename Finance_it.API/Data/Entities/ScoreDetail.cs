using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance_it.API.Data.Entities
{
    public class ScoreDetail
    {
        public int Id { get; set; }
        public int FinancialScoreId { get; set; }
        public virtual FinancialScore FinancialScore { get; set; } = new();
        [Required(ErrorMessage ="Criterion is required.")]
        public string Criterion { get; set; }= null!;
        [Required(ErrorMessage ="Criterion Value is required.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal CriterionValue { get; set; } 
    }
}

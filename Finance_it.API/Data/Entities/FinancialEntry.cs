using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance_it.API.Data.Entities
{
    public class FinancialEntry
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required(ErrorMessage ="Category is required.")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Required(ErrorMessage ="Amount is required.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage ="Transaction date is required.")]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public string Description { get; set; } = string.Empty;
    }
}

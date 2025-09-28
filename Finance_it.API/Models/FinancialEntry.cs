using System.ComponentModel.DataAnnotations;

namespace Finance_it.API.Models
{
    public class FinancialEntry
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required(ErrorMessage ="Financial type is required.")]
        [EnumDataType(typeof(FinancialType), ErrorMessage ="Invalid Financial Type")]
        public FinancialType Type { get; set; }
        [Required(ErrorMessage ="Amount is required.")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage ="Category is required.")]
        public string Category { get; set; } = null!;

        [Required(ErrorMessage ="Transaction date is required.")]
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; } = string.Empty;

    }
}

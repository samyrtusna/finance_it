using System.ComponentModel.DataAnnotations;

namespace Finance_it.API.Models
{
    public class FinancialEntry
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = new();
        [Required(ErrorMessage ="A Financial type is required.")]
        [EnumDataType(typeof(FinancialType), ErrorMessage ="Invalid Financial Type")]
        public FinancialType Type { get; set; }
        [Required(ErrorMessage ="An Amount is required.")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage ="A Category is required.")]
        public string Category { get; set; }=string.Empty;
       
        [Required(ErrorMessage ="A Transaction date is required.")]
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; } = string.Empty;

    }
}

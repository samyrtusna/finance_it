using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance_it.API.Data.Entities
{
    public class YearlyAgregate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required(ErrorMessage ="Year is required.")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Year income is required.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal YearIncome { get; set; }
        [Required(ErrorMessage = "Year expense is required.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal YearExpense { get; set; }
        [Required(ErrorMessage = "Year balance is required.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal YearBalance { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance_it.API.Data.Entities
{
    public class MonthlyAgregate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required(ErrorMessage ="year is required.")]
        public int Year { get; set; }
        [Required(ErrorMessage ="month is required.")]
        public string Month { get; set; }
        [Required(ErrorMessage ="month income is required.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal MonthIncome { get; set; }
        [Required(ErrorMessage ="month expense is required.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal MonthExpense { get; set; }
        [Required(ErrorMessage ="month balance is required.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal MonthBalance { get; set; }

    }
}

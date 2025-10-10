using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance_it.API.Data.Entities
{
    public class WeeklyAgregate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required(ErrorMessage ="WeekStartDay is required.")]
        public DateTime WeekStartDate { get; set; }
        [Required(ErrorMessage ="WeekEndDay is required.")]
        public DateTime WeekEndDate { get; set; }
        [Required(ErrorMessage ="Week income is required.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal WeekIncome { get; set; }
        [Required(ErrorMessage ="Week expense is required.")]
        [Column(TypeName = "decimal(10,2)")] 
        public decimal WeekExpense { get; set; }
        [Required(ErrorMessage ="Week balance is required.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal WeekBalance { get; set; }

    }
}

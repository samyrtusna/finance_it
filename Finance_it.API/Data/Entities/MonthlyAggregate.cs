using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance_it.API.Data.Entities
{
    public class MonthlyAggregate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required(ErrorMessage ="year is required.")]
        public int Year { get; set; }
        [Required(ErrorMessage ="month is required.")]
        public string Month { get; set; }
        [Required(ErrorMessage = "Monthly Agregate Name is required.")]
        [EnumDataType(typeof(AggregateName), ErrorMessage ="Invalid Agregate Name.")]
        public AggregateName AggregateName { get; set; }
        [Required(ErrorMessage ="Monthly agregate value is required.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal AggregateValue { get; set; } 
    }
}

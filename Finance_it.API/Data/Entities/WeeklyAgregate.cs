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
        [Required(ErrorMessage = "Weekly agregate name is required.")]
        [EnumDataType(typeof(AgregateName), ErrorMessage = "Invalid Agregate Name.")]
        public AgregateName AgregateName { get; set; }
        [Required(ErrorMessage = "Weekly agregate value is required.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal AgregateValue { get; set; }
    }
}

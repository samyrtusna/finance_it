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
        [Required(ErrorMessage ="Yearly agregate name is required.")]
        [EnumDataType(typeof(AgregateName), ErrorMessage = "Invalid Agregate Name.")]
        public AgregateName AgregateName { get; set; }
        [Required(ErrorMessage = "Yearly agregate value is required.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal AgregateValue { get; set; }
    }
}

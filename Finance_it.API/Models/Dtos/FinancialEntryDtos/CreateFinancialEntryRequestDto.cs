using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance_it.API.Models.Dtos.FinancialEntryDtos
{
    public class CreateFinancialEntryRequestDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "CategoryId is required.")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Amount is required.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

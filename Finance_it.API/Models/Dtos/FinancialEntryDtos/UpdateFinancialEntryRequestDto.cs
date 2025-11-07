using Finance_it.API.Data.Entities;

namespace Finance_it.API.Models.Dtos.FinancialEntryDtos
{
    public class UpdateFinancialEntryRequestDto
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public decimal Amount { get; set; }     
        public string Description { get; set; } 
    }
}

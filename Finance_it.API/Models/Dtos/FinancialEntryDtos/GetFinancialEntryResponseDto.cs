using Finance_it.API.Data.Entities;
using Finance_it.API.Models.Dtos.CategoryDtos;

namespace Finance_it.API.Models.Dtos.FinancialEntryDtos
{
    public class GetFinancialEntryResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public BasicCategoryDto Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
    }
}

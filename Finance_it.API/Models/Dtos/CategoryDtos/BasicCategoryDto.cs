using Finance_it.API.Data.Entities;

namespace Finance_it.API.Models.Dtos.CategoryDtos
{
    public class BasicCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public FinancialType Type { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}

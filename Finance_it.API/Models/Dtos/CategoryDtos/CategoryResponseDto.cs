using System.ComponentModel.DataAnnotations;
using Finance_it.API.Data.Entities;

namespace Finance_it.API.Models.Dtos.CategoryDtos
{
    public class CategoryResponseDto
    {
        public  int Id { get; set; }        
        public string Name { get; set; } = null!;        
        public FinancialType Type { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual ICollection<CategoryResponseDto> SubCategories { get; set; } = new List<CategoryResponseDto>();
    }
}

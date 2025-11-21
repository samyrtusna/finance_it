using Finance_it.API.Models.Dtos.CategoryDtos;

namespace Finance_it.API.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync(int userId);
    }
}

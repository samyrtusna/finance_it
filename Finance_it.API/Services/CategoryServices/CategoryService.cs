using AutoMapper;
using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Models.Dtos.CategoryDtos;
using Finance_it.API.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Finance_it.API.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync(int userId)
        {
            if(userId <= 0)
            {
                throw new ArgumentException("Invalid userId");
            }
            var categories = await _categoryRepository
                .GetAllByFilterAsync(
                    c => c.UserId == null || c.UserId == userId
                    ) ?? throw new NotFoundException("categories not found");
            

            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
        }
    }
}

using Finance_it.API.Models.Dtos.CategoryDtos;
using Finance_it.API.Services.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance_it.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync(UserId);
            return Ok(categories);
        }
    }
}

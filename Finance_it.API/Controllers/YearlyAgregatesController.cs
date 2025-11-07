using System.Runtime.CompilerServices;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.YearlyAgregateDtos;
using Finance_it.API.Services.YearlyAgregatesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance_it.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class YearlyAgregatesController : BaseController
    {
        private readonly IYearlyAgregatesService _service;

        public YearlyAgregatesController(IYearlyAgregatesService service)
        {
            _service = service;
        }

        [HttpGet("CurrentYearAgregates")]
        public async Task<ActionResult<ApiResponseDto<CurrentYearAgregatesDto>>> GetCurrentYearAgregates()
        {
            var response = await _service.GetCurrentYearAgregatesAsync(UserId);

            return Ok(response);
        }

        [HttpGet("AllYearlyAgregates")]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<YearlyAgregateResponseDto>>>> GetAllYearlyAgregates()
        {
            var response = await _service.GetAllYearlyAgregatesAsync(UserId);

            return Ok(response);
        }

        [HttpGet("YearlyAgregatesByYear")]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<YearlyAgregateResponseDto>>>> GetYearlyAgregatesByYear(DateTime date)
        {
            var response = await _service.GetYearlyAgregatesByYearAsync(UserId, date);

            return Ok(response);
        }
    }
}

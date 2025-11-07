using System.Security.Claims;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.FinancialEntryDtos;
using Finance_it.API.Services.FinancialEntryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance_it.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinancialEntryController : BaseController
    {
        private readonly IFinancialEntryService _service;

        public FinancialEntryController(IFinancialEntryService service)
        {
            _service = service;
        }

        [HttpPost("new")]
        public async Task<ActionResult<ApiResponseDto<CreateFinancialEntryResponseDto>>> AddNewFinancialEntry(CreateFinancialEntryRequestDto entry)
        {
            var response = await _service.AddFinancialEntryAsync(entry);

            return Ok(response);
        }

        [HttpGet("all")]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<GetFinancialEntryResponseDto>>>> GetAllFinancialEntries()
        {
            var response = await _service.GetAllFinancialEntriesAsync(UserId);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseDto<GetFinancialEntryResponseDto>>> GetFinancialEntryById(int id)
        {
            var response = await _service.GetFinancialEntryByIdAsync(id);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseDto<GetFinancialEntryResponseDto>>> UpdateFinancialEntry(UpdateFinancialEntryRequestDto dto, int id)
        {
            var response = await _service.UpdateFinancialEntryAsync(dto, id);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseDto<ConfirmationResponseDto>>> DeleteFinancialEntry(int id)
        {
            var response = await _service.DeleteFinancialEntryAsync(id);

            return Ok(response);
        }
    }
}

using System.Security.Claims;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.UserDtos;
using Finance_it.API.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance_it.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponseDto<string>>> Register(RegisterRequestDto dto)
        {
            var response = await _service.RegisterAsync(dto);

            return Ok(response);
        }


        [Authorize]
        [HttpPost("change-password")]
        public async Task<ActionResult<ApiResponseDto<ConfirmationResponseDto>>> ChangePassword(PasswordChangeRequestDto dto)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? throw new BadHttpRequestException("Email claim not found.");

            var response = await _service.ChangePassword(dto, email);

            return Ok(response);

        }
    }
}

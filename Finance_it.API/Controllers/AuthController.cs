using System.Security.Claims;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.UserDtos;
using Finance_it.API.Services.AuthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance_it.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponseDto<string>>> Login(LoginRequestDto dto)
        {
            var response = await _service.LoginAsync(dto);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult<ApiResponseDto<ConfirmationResponseDto>>> Logout()
        {
            var claimValue = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(claimValue, out int id))
            {
                throw new BadRequestException("Invalid user ID in token.");
            }
            var response = await _service.LogoutAsync(id);

            return Ok(response);
        }


        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApiResponseDto<string>>> RefreshToken()
        {
            var response= await _service.RefreshTokenAsync();
            return Ok(response);
        }
    }
}

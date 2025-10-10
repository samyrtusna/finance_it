using System.Security.Claims;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.UserDtos;
using Finance_it.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance_it.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponseDto<AuthenticationResponseDto>>> Login(LoginRequestDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(new ApiResponseDto<AuthenticationResponseDto>(200, result));
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
            var result = await _authService.LogoutAsync(id);

            return Ok(new ApiResponseDto<ConfirmationResponseDto>(200, result));
        }


        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApiResponseDto<AuthenticationResponseDto>>> RefreshToken([FromBody] string token)
        {
            var result = await _authService.RefreshTokenAsync(token);
            return Ok(new ApiResponseDto<AuthenticationResponseDto>(200, result));
        }
    }
}

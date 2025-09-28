using System.Security.Claims;
using Castle.Components.DictionaryAdapter.Xml;
using Finance_it.API.Dtos.ApiResponsesDtos;
using Finance_it.API.Dtos.UserDtos;
using Finance_it.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            try
            {
                var result = await _authService.LoginAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto<AuthenticationResponseDto>(500, new List<string> { "An error occurred while processing your request.", ex.Message }));
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult<ApiResponseDto<ConfirmationResponseDto>>> Logout()
        {
            try
            {
                var claimValue = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if(!int.TryParse(claimValue, out int id))
                {
                    return BadRequest(new ApiResponseDto<ConfirmationResponseDto>(400, new List<string> { "Invalid user ID in token." }));
                }
                var result = await _authService.LogoutAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto<ConfirmationResponseDto>(500, new List<string> { "An error occurred while processing your request.", ex.Message }));
            }
        }

    }
}

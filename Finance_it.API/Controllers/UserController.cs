using System.Security.Claims;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.UserDtos;
using Finance_it.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance_it.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServices;

        public UserController(IUserService userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponseDto<AuthenticationResponseDto>>> Register(RegisterRequestDto dto)
        {
            var result = await _userServices.RegisterAsync(dto);

            return Ok(new ApiResponseDto<AuthenticationResponseDto>(200, result));
        }


        [Authorize]
        [HttpPost("change-password")]
        public async Task<ActionResult<ApiResponseDto<ConfirmationResponseDto>>> ChangePassword(PasswordChangeRequestDto dto)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? throw new BadHttpRequestException("Email claim not found.");

            var result = await _userServices.ChangePassword(dto, email);

            return Ok(new ApiResponseDto<ConfirmationResponseDto>(200, result));

        }
    }
}

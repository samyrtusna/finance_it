using Finance_it.API.Dtos.ApiResponsesDtos;
using Finance_it.API.Dtos.UserDtos;
using Finance_it.API.Services;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<ApiResponseDto<AuthenticationResponseDto>>> Register (RegisterRequestDto dto)
        {
            try
            {
                var result = await _userServices.RegisterAsync(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto<AuthenticationResponseDto>(500, new List<string> { "An error occurred while processing your request.", ex.Message }));
            }
        }
    }
}

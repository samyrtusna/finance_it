using Finance_it.API.Dtos.ApiResponsesDtos;
using Finance_it.API.Dtos.UserDtos;

namespace Finance_it.API.Services
{
    public interface IUserService
    {
        Task<ApiResponseDto<AuthenticationResponseDto>> RegisterAsync(RegisterRequestDto dto);   
    }
}

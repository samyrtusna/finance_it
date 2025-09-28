using Finance_it.API.Dtos.ApiResponsesDtos;
using Finance_it.API.Dtos.UserDtos;
using Finance_it.API.Infrastructure.Security;
using Finance_it.API.Repositories.CustomRepositories;

namespace Finance_it.API.Services
{
    public interface IAuthService
    {
        Task<ApiResponseDto<AuthenticationResponseDto>> LoginAsync(LoginRequestDto dto);
        Task<ApiResponseDto<AuthenticationResponseDto>> RefreshTokenAsync(string token);
        Task<ApiResponseDto<ConfirmationResponseDto>> LogoutAsync(int id); 
    }
}

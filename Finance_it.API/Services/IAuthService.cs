using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.UserDtos;

namespace Finance_it.API.Services
{
    public interface IAuthService
    {
        Task<AuthenticationResponseDto> LoginAsync(LoginRequestDto dto);
        Task<AuthenticationResponseDto> RefreshTokenAsync(string token);
        Task<ConfirmationResponseDto> LogoutAsync(int id); 
    }
}

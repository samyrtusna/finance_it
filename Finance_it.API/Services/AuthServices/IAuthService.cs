using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.UserDtos;

namespace Finance_it.API.Services.AuthServices
{
    public interface IAuthService
    {
        Task<AuthenticationResponseDto> LoginAsync(LoginRequestDto dto);
        Task<AuthenticationResponseDto> RefreshTokenAsync();
        Task<ConfirmationResponseDto> LogoutAsync(int id); 
    }
}

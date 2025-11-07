using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.UserDtos;

namespace Finance_it.API.Services.UserServices
{
    public interface IUserService
    {
        Task<AuthenticationResponseDto> RegisterAsync(RegisterRequestDto dto);
        Task<ConfirmationResponseDto> ChangePassword(PasswordChangeRequestDto dto, string email);   
    }
}

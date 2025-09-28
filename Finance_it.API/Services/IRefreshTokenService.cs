using Finance_it.API.Dtos.ApiResponsesDtos;
using Finance_it.API.Dtos.RefreshTokenDtos;
using Finance_it.API.Dtos.UserDtos;
using Finance_it.API.Models;

namespace Finance_it.API.Services
{
    public interface IRefreshTokenService
    {
        Task<ApiResponseDto<RefreshTokenResponseDto>> AddRefreshTokenAsync(int id);
        Task<ApiResponseDto<RefreshTokenResponseDto>> GetRefreshTokenAsync(string token);
        Task RevokeRefreshTokenAsync(RefreshToken refreshToken);
    }
}

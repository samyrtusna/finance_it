using Finance_it.API.Data.Entities;
using Finance_it.API.Models.Dtos.RefreshTokenDtos;

namespace Finance_it.API.Services
{
    public interface IRefreshTokenService
    {
        Task<RefreshTokenResponseDto> AddRefreshTokenAsync(int id);
        //Task<RefreshTokenResponseDto> GetRefreshTokenAsync(string token);
        void RevokeRefreshTokenAsync(RefreshToken refreshToken);
    }
}

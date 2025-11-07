using Finance_it.API.Data.Entities;
using Finance_it.API.Models.Dtos.RefreshTokenDtos;

namespace Finance_it.API.Services.RefreshTokenServices
{
    public interface IRefreshTokenService
    {
        Task<RefreshTokenResponseDto> AddRefreshTokenAsync(int id);
        void RevokeRefreshTokenAsync(RefreshToken refreshToken);
        void SetTokenToCookies(string refreshToken, HttpResponse response); 
    }
}

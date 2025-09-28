
using Finance_it.API.Dtos.ApiResponsesDtos;
using Finance_it.API.Dtos.RefreshTokenDtos;
using Finance_it.API.Infrastructure.Security;
using Finance_it.API.Models;
using Finance_it.API.Repositories.CustomRepositories;

namespace Finance_it.API.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IConfiguration _config;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository; 

        public RefreshTokenService(IJwtTokenGenerator tokenGenerator, IRefreshTokenRepository refreshTokenRepository, IConfiguration config )
        {
            _config = config;
            _tokenGenerator = tokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<ApiResponseDto<RefreshTokenResponseDto>> AddRefreshTokenAsync(int id)
        {
            var refreshToken = _tokenGenerator.GenerateRefreshToken();
            int tokenExpiration = int.Parse(_config["Jwt:RefreshTokenExpiration"]!);

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = id,
                ExpiresAt = DateTime.UtcNow.AddDays(tokenExpiration)
            };
            await _refreshTokenRepository.AddAsync(refreshTokenEntity);
            await _refreshTokenRepository.SaveAsync();

            var response = new RefreshTokenResponseDto
            {
                Token = refreshTokenEntity.Token,
                UserId = refreshTokenEntity.UserId,
                ExpiresAt = refreshTokenEntity.ExpiresAt
            };


            return new ApiResponseDto<RefreshTokenResponseDto>(200, response);

        }

        public async Task<ApiResponseDto<RefreshTokenResponseDto>> GetRefreshTokenAsync(string token)
        {
            var refreshToken = await _refreshTokenRepository.GetRefreshTokenByTokenAsync(token);

            if(refreshToken == null)
            {
                return new ApiResponseDto<RefreshTokenResponseDto>(404,new List<string> { "Refresh token not found." });
            }
            var response = new RefreshTokenResponseDto
            {
                Token = refreshToken.Token,
                UserId = refreshToken.UserId,
                CreatedAt = refreshToken.CreatedAt,
                ExpiresAt = refreshToken.ExpiresAt,
                IsRevoked = refreshToken.IsRevoked,
                RevokedAt = refreshToken.RevokedAt
            };

            return new ApiResponseDto<RefreshTokenResponseDto>(200, response);
        }

        public async Task RevokeRefreshTokenAsync(RefreshToken refreshToken)
        {
            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;

            _refreshTokenRepository.Update(refreshToken);
            await _refreshTokenRepository.SaveAsync();
        }
    }
}

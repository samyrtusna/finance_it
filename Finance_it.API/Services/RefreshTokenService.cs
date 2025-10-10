using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Security;
using Finance_it.API.Models.Dtos.RefreshTokenDtos;
using Finance_it.API.Repositories.GenericRepositories;

namespace Finance_it.API.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IConfiguration _config;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IGenericRepository<RefreshToken> _refreshTokenRepository; 

        public RefreshTokenService(IJwtTokenGenerator tokenGenerator, IGenericRepository<RefreshToken> refreshTokenRepository, IConfiguration config )
        {
            _config = config;
            _tokenGenerator = tokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<RefreshTokenResponseDto> AddRefreshTokenAsync(int id)
        {
            ArgumentNullException.ThrowIfNull(id, $"the argument {nameof(id)} is null");

            var refreshToken = _tokenGenerator.GenerateRefreshToken();
            int tokenExpiration = int.Parse(_config["Jwt:RefreshTokenExpiration"]!);

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = id,
                ExpiresAt = DateTime.UtcNow.AddDays(tokenExpiration)
            };
            await _refreshTokenRepository.AddAsync(refreshTokenEntity);

            return new RefreshTokenResponseDto
            {
                Token = refreshTokenEntity.Token,
                UserId = refreshTokenEntity.UserId,
                ExpiresAt = refreshTokenEntity.ExpiresAt
            };
        }

        public void RevokeRefreshTokenAsync(RefreshToken refreshToken)
        {
            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;

            _refreshTokenRepository.Update(refreshToken);
        }
    }
}

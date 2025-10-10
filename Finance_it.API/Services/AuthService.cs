using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Infrastructure.Security;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.UserDtos;
using Finance_it.API.Repositories.GenericRepositories;

namespace Finance_it.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<RefreshToken> _refreshTokenRepository;
        private readonly IRefreshTokenService _tokenServices;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthService(IGenericRepository<User> userRepository, IRefreshTokenService tokenServices, IJwtTokenGenerator tokenGenerator, IGenericRepository<RefreshToken> refreshTokenRepository)
        {
            _userRepository = userRepository;
            _tokenServices = tokenServices;
            _tokenGenerator = tokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthenticationResponseDto> LoginAsync(LoginRequestDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto, $"the argument {nameof(dto)} is null");

            var user = await _userRepository.GetByFilterAsync(u => u.Email.Equals(dto.Email))?? throw new NotFoundException("User not found.");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

            if (!isPasswordValid)
            {
                throw new UnauthorizedException("Invalid password.");
            }
            string accessToken = _tokenGenerator.GenerateAccessToken(user);
            var NewRefreshToken = await _tokenServices.AddRefreshTokenAsync(user.Id);

            user.LastLogin = DateTime.UtcNow;
            _userRepository.Update(user);

            await _userRepository.SaveAsync();

            return new AuthenticationResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = NewRefreshToken.Token,
            };
        }

        public async Task<AuthenticationResponseDto> RefreshTokenAsync(string token)
        {
            ArgumentNullException.ThrowIfNull(token, $"the argument {nameof(token)} is null");

            var existingRefreshToken = await _refreshTokenRepository.GetByFilterAsync(r => r.Token.Equals(token)) ?? throw new NotFoundException("Refresh token not found.");

            if (existingRefreshToken.IsRevoked || existingRefreshToken.ExpiresAt <= DateTime.UtcNow)
            {
                throw new UnauthorizedException("Refresh token is invalid or expired.");
            }

            var user = await _userRepository.GetByIdAsync(existingRefreshToken.UserId)?? throw new NotFoundException("User not found.");

            var accessToken = _tokenGenerator.GenerateAccessToken(user);
            var refreshToken = await _tokenServices.AddRefreshTokenAsync(user.Id);

            _tokenServices.RevokeRefreshTokenAsync(existingRefreshToken);

            await _userRepository.SaveAsync();

            return new AuthenticationResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<ConfirmationResponseDto> LogoutAsync(int userId)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");

            var user = await _userRepository.GetByIdAsync(userId)?? throw new NotFoundException("User not found.");
          
            var refreshTokens = user?.RefreshTokens.Where(rt => !rt.IsRevoked && rt.ExpiresAt > DateTime.UtcNow).ToList();
            if (refreshTokens != null )
            {
                foreach (var token in refreshTokens)
                {
                    _tokenServices.RevokeRefreshTokenAsync(token);
                }
                await _userRepository.SaveAsync();
            }

            return new ConfirmationResponseDto { Message = "User logged out successfully." };
        }
    }
}

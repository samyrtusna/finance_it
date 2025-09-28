using Finance_it.API.Dtos.ApiResponsesDtos;
using Finance_it.API.Dtos.UserDtos;
using Finance_it.API.Infrastructure.Security;
using Finance_it.API.Repositories.CustomRepositories;

namespace Finance_it.API.Services
{
    public class AuthService : IAuthService
    {
         private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenService _TokenServices;
        private readonly IJwtTokenGenerator _TokenGenerator;

        public AuthService(IUserRepository userRepository, IRefreshTokenService tokenServices, IJwtTokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _TokenServices = tokenServices;
            _TokenGenerator = tokenGenerator;
        }

        public async Task<ApiResponseDto<AuthenticationResponseDto>> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userRepository.GetUserByEmailAsync(dto.Email);

            if (user == null)
            {
                return new ApiResponseDto<AuthenticationResponseDto>(404, "User not found.");
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

            if (!isPasswordValid)
            {
                return new ApiResponseDto<AuthenticationResponseDto>(401, "Invalid password.");
            }
            string accessToken = _TokenGenerator.GenerateAccessToken(user);
            var NewRefreshToken = await _TokenServices.AddRefreshTokenAsync(user.Id);

            user.LastLogin = DateTime.UtcNow;
            _userRepository.Update(user);
            await _userRepository.SaveAsync();

            var response = new AuthenticationResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = NewRefreshToken.Data.Token,
            };
            return new ApiResponseDto<AuthenticationResponseDto>(200, response);
        }

        public async Task<ApiResponseDto<AuthenticationResponseDto>> RefreshTokenAsync(string token)
        {
            var existingRefreshToken = await _TokenServices.GetRefreshTokenAsync(token);

            if (existingRefreshToken == null || existingRefreshToken.StatusCode == 404)
            {
                return new ApiResponseDto<AuthenticationResponseDto>(404, "Refresh token not found.");
            }

            if (existingRefreshToken.Data.IsRevoked || existingRefreshToken.Data.ExpiresAt <= DateTime.UtcNow)
            {
                return new ApiResponseDto<AuthenticationResponseDto>(401, "Refresh token is invalid or expired.");
            }

            var user = await _userRepository.GetByIdAsync(existingRefreshToken.Data.UserId);

            if (user == null)
            {
                return new ApiResponseDto<AuthenticationResponseDto>(404, "User not found.");
            }
            var accessToken = _TokenGenerator.GenerateAccessToken(user);
            var refreshToken = await _TokenServices.AddRefreshTokenAsync(user.Id);

            var response = new AuthenticationResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Data.Token,
            };

            return new ApiResponseDto<AuthenticationResponseDto>(200, response);
        }

        public async Task<ApiResponseDto<ConfirmationResponseDto>> LogoutAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return new ApiResponseDto<ConfirmationResponseDto>(404, "User not found.");
            }
            var refreshTokens = user?.RefreshTokens.Where(rt => !rt.IsRevoked && rt.ExpiresAt > DateTime.UtcNow).ToList();
            if (refreshTokens != null )
            {
                foreach (var token in refreshTokens)
                {
                    token.IsRevoked = true;
                    token.RevokedAt = DateTime.UtcNow;
                }
                _userRepository.Update(user);
                await _userRepository.SaveAsync();
            }
            
            return new ApiResponseDto<ConfirmationResponseDto>(200, new ConfirmationResponseDto { Message = "User logged out successfully." });
        }
    }
}

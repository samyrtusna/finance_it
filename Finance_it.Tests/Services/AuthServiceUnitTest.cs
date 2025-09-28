using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance_it.API.Dtos.ApiResponsesDtos;
using Finance_it.API.Dtos.RefreshTokenDtos;
using Finance_it.API.Dtos.UserDtos;
using Finance_it.API.Infrastructure.Security;
using Finance_it.API.Models;
using Finance_it.API.Repositories.CustomRepositories;
using Finance_it.API.Services;
using Moq;
using Xunit;

namespace Finance_it.Tests.Services
{
    public class AuthServiceUnitTest
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IRefreshTokenService> _mockRefreshTokenService;
        private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator;
        private readonly AuthService _authService;

        public AuthServiceUnitTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockRefreshTokenService = new Mock<IRefreshTokenService>();
            _mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();

            _authService = new AuthService(
                _mockUserRepository.Object,
                _mockRefreshTokenService.Object,
                _mockJwtTokenGenerator.Object
            );
        }

        
        [Fact]
        public async Task LoginAsync_ShouldReturn404_WhenUserNotFound()
        {
            // Arrange
            var dto = new LoginRequestDto { Email = "notfound@test.com", Password = "pass" };
            _mockUserRepository.Setup(r => r.GetUserByEmailAsync(dto.Email))
                               .ReturnsAsync((User?)null);

            // Act
            var result = await _authService.LoginAsync(dto);

            // Assert
            Assert.Equal(404, result.StatusCode);
            Assert.Null(result.Data);
            Assert.Contains("User not found.", result.Errors);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturn401_WhenPasswordInvalid()
        {
            // Arrange
            var dto = new LoginRequestDto { Email = "user@test.com", Password = "wrongpass" };
            var user = new User { Id = 1, Email = dto.Email, Password = BCrypt.Net.BCrypt.HashPassword("correctpass") };

            _mockUserRepository.Setup(r => r.GetUserByEmailAsync(dto.Email)).ReturnsAsync(user);

            // Act
            var result = await _authService.LoginAsync(dto);

            // Assert
            Assert.Equal(401, result.StatusCode);
            Assert.Null(result.Data);
            Assert.Contains("Invalid password.", result.Errors);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturn200_WhenLoginSuccessful()
        {
            // Arrange
            var dto = new LoginRequestDto { Email = "user@test.com", Password = "correctpass" };
            var user = new User { Id = 1, Email = dto.Email, Password = BCrypt.Net.BCrypt.HashPassword("correctpass") };

            _mockUserRepository.Setup(r => r.GetUserByEmailAsync(dto.Email)).ReturnsAsync(user);
            _mockJwtTokenGenerator.Setup(g => g.GenerateAccessToken(user)).Returns("access-token");

            _mockRefreshTokenService.Setup(s => s.AddRefreshTokenAsync(user.Id))
                .ReturnsAsync(new ApiResponseDto<RefreshTokenResponseDto>(200, new RefreshTokenResponseDto
                {
                    Token = "refresh-token",
                    UserId = user.Id,
                    ExpiresAt = DateTime.UtcNow.AddDays(7)
                }));

            // Act
            var result = await _authService.LoginAsync(dto);

            // Assert
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal("access-token", result.Data.AccessToken);
            Assert.Equal("refresh-token", result.Data.RefreshToken);
        }

       
        [Fact]
        public async Task RefreshTokenAsync_ShouldReturn404_WhenRefreshTokenNotFound()
        {
            // Arrange
            string token = "invalid";
            _mockRefreshTokenService.Setup(s => s.GetRefreshTokenAsync(token))
                .ReturnsAsync(new ApiResponseDto<RefreshTokenResponseDto>(404, "Refresh token not found."));

            // Act
            var result = await _authService.RefreshTokenAsync(token);

            // Assert
            Assert.Equal(404, result.StatusCode);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task RefreshTokenAsync_ShouldReturn401_WhenTokenRevokedOrExpired()
        {
            // Arrange
            string token = "expired-token";
            _mockRefreshTokenService.Setup(s => s.GetRefreshTokenAsync(token))
                .ReturnsAsync(new ApiResponseDto<RefreshTokenResponseDto>(200, new RefreshTokenResponseDto
                {
                    Token = token,
                    UserId = 1,
                    IsRevoked = true,
                    ExpiresAt = DateTime.UtcNow.AddDays(-1)
                }));

            // Act
            var result = await _authService.RefreshTokenAsync(token);

            // Assert
            Assert.Equal(401, result.StatusCode);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task RefreshTokenAsync_ShouldReturn200_WhenTokenValid()
        {
            // Arrange
            string token = "valid-token";
            var refreshTokenDto = new RefreshTokenResponseDto
            {
                Token = token,
                UserId = 1,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            var user = new User { Id = 1, Email = "user@test.com" };

            _mockRefreshTokenService.Setup(s => s.GetRefreshTokenAsync(token))
                .ReturnsAsync(new ApiResponseDto<RefreshTokenResponseDto>(200, refreshTokenDto));

            _mockUserRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);

            _mockJwtTokenGenerator.Setup(g => g.GenerateAccessToken(user)).Returns("new-access-token");

            _mockRefreshTokenService.Setup(s => s.AddRefreshTokenAsync(user.Id))
                .ReturnsAsync(new ApiResponseDto<RefreshTokenResponseDto>(200, new RefreshTokenResponseDto
                {
                    Token = "new-refresh-token",
                    UserId = user.Id,
                    ExpiresAt = DateTime.UtcNow.AddDays(7)
                }));

            // Act
            var result = await _authService.RefreshTokenAsync(token);

            // Assert
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal("new-access-token", result.Data.AccessToken);
            Assert.Equal("new-refresh-token", result.Data.RefreshToken);
        }

        
        [Fact]
        public async Task LogoutAsync_ShouldReturn404_WhenUserNotFound()
        {
            // Arrange
            _mockUserRepository.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((User)null);

            // Act
            var result = await _authService.LogoutAsync(99);

            // Assert
            Assert.Equal(404, result.StatusCode);
            Assert.Null(result.Data);
            Assert.Contains("User not found.", result.Errors);
        }

        [Fact]
        public async Task LogoutAsync_ShouldReturn200_WhenLogoutSuccessful()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                RefreshTokens = new List<RefreshToken>
                {
                    new RefreshToken
                    {
                        Token = "active-token",
                        ExpiresAt = DateTime.UtcNow.AddDays(5),
                        IsRevoked = false
                    }
                }
            };

            _mockUserRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);
            _mockUserRepository.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _authService.LogoutAsync(1);

            // Assert
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal("User logged out successfully.", result.Data.Message);
            Assert.True(user.RefreshTokens.All(rt => rt.IsRevoked));
        }
    }
}

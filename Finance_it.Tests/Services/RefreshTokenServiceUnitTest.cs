using Microsoft.Extensions.Configuration;
using Finance_it.API.Infrastructure.Security;
using Finance_it.API.Repositories.CustomRepositories;
using Finance_it.API.Services;
using Moq;
using Finance_it.API.Models;

namespace Finance_it.Tests.Services
{
    public class RefreshTokenServiceUnitTest
    {
        private readonly Mock<IRefreshTokenRepository> _mockRefreshTokenRepository;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator;

        private readonly RefreshTokenService _refreshTokenService;

        public RefreshTokenServiceUnitTest()
        {
            _mockRefreshTokenRepository = new Mock<IRefreshTokenRepository>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            _mockConfiguration.Setup(config => config["Jwt:RefreshTokenExpiration"]).Returns("7");


            _refreshTokenService = new RefreshTokenService(_mockJwtTokenGenerator.Object, _mockRefreshTokenRepository.Object, _mockConfiguration.Object);
        }

        [Fact]
        public async Task AddRefreshTokenAsync_ShouldReturnApiResponseDtoWithRefreshTokenResponseDto()
        {
            // Arrange
            int userId = 1;
            string generatedToken = "sample_refresh_token";

            _mockJwtTokenGenerator.Setup(gen => gen.GenerateRefreshToken()).Returns(generatedToken);

            _mockRefreshTokenRepository.Setup(repo => repo.AddAsync(It.IsAny<RefreshToken>())).Returns(Task.CompletedTask);
            _mockRefreshTokenRepository.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _refreshTokenService.AddRefreshTokenAsync(userId);

            // Assert
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal(userId, result.Data.UserId);
            Assert.Equal(generatedToken, result.Data.Token);

            _mockJwtTokenGenerator.Verify(gen => gen.GenerateRefreshToken(), Times.Once);
            _mockRefreshTokenRepository.Verify(repo => repo.AddAsync(It.IsAny<RefreshToken>()), Times.Once);
            _mockRefreshTokenRepository.Verify(repo => repo.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GetRefreshTokenAsync_ShouldReturnApiResponseDtoWithRefreshTokenResponseDto_WhenTokenExists()
        {
            // Arrange
            string token = "valid_token";
            var refreshTokenEntity = new RefreshToken
            {
                Token = token,
                UserId = 1,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            _mockRefreshTokenRepository.Setup(repo => repo.GetRefreshTokenByTokenAsync(token)).ReturnsAsync(refreshTokenEntity);

            // Act
            var result = await _refreshTokenService.GetRefreshTokenAsync(token);

            // Assert
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal(token, result.Data.Token);
            Assert.Equal(refreshTokenEntity.UserId, result.Data.UserId);
        }

        [Fact]
        public async Task GetRefreshTokenAsync_ShouldReturnApiResponseDtoWith404StatusCode_WhenTokenDoesNotExist()
        {
            // Arrange
            string token = "invalid_token";
            _mockRefreshTokenRepository.Setup(repo => repo.GetRefreshTokenByTokenAsync(token)).ReturnsAsync((RefreshToken?)null);

            // Act
            var result = await _refreshTokenService.GetRefreshTokenAsync(token);

            // Assert
            Assert.Equal(404, result.StatusCode);
            Assert.Null(result.Data);
            Assert.Contains("Refresh token not found.", result.Errors);
        }

        [Fact]
        public async Task RevokeRefreshTokenAsync_ShouldUpdateToken() 
        {
            // Arrange
            var refreshToken = new RefreshToken
            {
                Token = "test-token",
                UserId = 1,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };
            _mockRefreshTokenRepository.Setup(repo => repo.Update(refreshToken));
            _mockRefreshTokenRepository.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            await _refreshTokenService.RevokeRefreshTokenAsync(refreshToken);

            // Assert
            Assert.True(refreshToken.IsRevoked);
            Assert.NotNull(refreshToken.RevokedAt);

            _mockRefreshTokenRepository.Verify(repo => repo.Update(refreshToken), Times.Once);
            _mockRefreshTokenRepository.Verify(repo => repo.SaveAsync(), Times.Once);
        }
    }
}

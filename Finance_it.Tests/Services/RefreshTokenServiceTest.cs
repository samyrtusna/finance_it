using System;
using System.Threading.Tasks;
using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Security;
using Finance_it.API.Repositories.GenericRepositories;
using Finance_it.API.Services.RefreshTokenServices;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Finance_it.Tests.Services
{
    public class RefreshTokenServiceTests
    {
        private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
        private readonly Mock<IGenericRepository<RefreshToken>> _refreshTokenRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly RefreshTokenService _refreshTokenService;

        public RefreshTokenServiceTests()
        {
            _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            _refreshTokenRepositoryMock = new Mock<IGenericRepository<RefreshToken>>();
            _configurationMock = new Mock<IConfiguration>();

            _configurationMock.Setup(c => c["Jwt:RefreshTokenExpiration"]).Returns("7");

            _refreshTokenService = new RefreshTokenService(
                _jwtTokenGeneratorMock.Object,
                _refreshTokenRepositoryMock.Object,
                _configurationMock.Object
            );
        }

        [Fact]
        public async Task AddRefreshTokenAsync_ShouldReturnRefreshTokenResponse_WhenCalledWithValidId()
        {
            // Arrange
            var userId = 1;
            var fakeToken = "fake-refresh-token";

            _jwtTokenGeneratorMock.Setup(j => j.GenerateRefreshToken()).Returns(fakeToken);

            // Act
            var result = await _refreshTokenService.AddRefreshTokenAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(fakeToken, result.Token);
            Assert.Equal(userId, result.UserId);
            Assert.True(result.ExpiresAt > DateTime.UtcNow);

            _refreshTokenRepositoryMock.Verify(r => r.AddAsync(It.IsAny<RefreshToken>()), Times.Once);
        }

        [Fact]
        public async Task AddRefreshTokenAsync_ShouldThrowException_WhenIdIsZero()
        {
            // Arrange
            int invalidUserId = 0;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _refreshTokenService.AddRefreshTokenAsync(invalidUserId)
            );
        }

        [Fact]
        public void RevokeRefreshTokenAsync_ShouldMarkTokenAsRevoked()
        {
            // Arrange
            var refreshToken = new RefreshToken
            {
                Token = "old-token",
                UserId = 1,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            // Act
            _refreshTokenService.RevokeRefreshTokenAsync(refreshToken);

            // Assert
            Assert.True(refreshToken.IsRevoked);
            Assert.NotNull(refreshToken.RevokedAt);

            _refreshTokenRepositoryMock.Verify(r => r.Update(refreshToken), Times.Once);
        }
    }
}

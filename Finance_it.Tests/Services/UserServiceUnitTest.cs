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
    public class UserServiceUnitTest
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IRefreshTokenService> _mockRefreshTokenService;
        private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator;
        private readonly UserService _userService;

        public UserServiceUnitTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockRefreshTokenService = new Mock<IRefreshTokenService>();
            _mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();

            _userService = new UserService(
                _mockUserRepository.Object,
                _mockRefreshTokenService.Object,
                _mockJwtTokenGenerator.Object
            );
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturn400_WhenEmailAlreadyExists()
        {
            // Arrange
            var dto = new RegisterRequestDto
            {
                Name = "Samir",
                Email = "samir@gmail.com",
                Password = "test123",
                Role = Role.User
            };

            _mockUserRepository.Setup(r => r.GetUserByEmailAsync(dto.Email))
                .ReturnsAsync(new User { Email = dto.Email });

            // Act
            var result = await _userService.RegisterAsync(dto);

            // Assert
            Assert.Equal(400, result.StatusCode);
            Assert.Null(result.Data);
            Assert.Contains("Email is already in use.", result.Errors);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturn201_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var dto = new RegisterRequestDto
            {
                Name = "Mayas",
                Email = "mayas@gmail.com",
                Password = "password123",
                Role = Role.User
            };

            _mockUserRepository.Setup(r => r.GetUserByEmailAsync(dto.Email))
                .ReturnsAsync((User)null!);

            _mockJwtTokenGenerator.Setup(t => t.GenerateAccessToken(It.IsAny<User>()))
                .Returns("fake-access-token");

            _mockRefreshTokenService.Setup(s => s.AddRefreshTokenAsync(It.IsAny<int>()))
                .ReturnsAsync(new ApiResponseDto<RefreshTokenResponseDto>(200, new RefreshTokenResponseDto
                {
                    Token = "fake-refresh-token",
                    UserId = 1
                }));

            // Act
            var result = await _userService.RegisterAsync(dto);

            // Assert
            Assert.Equal(201, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal("fake-access-token", result.Data.AccessToken);
            Assert.Equal("fake-refresh-token", result.Data.RefreshToken);

            _mockUserRepository.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
            _mockUserRepository.Verify(r => r.SaveAsync(), Times.Once);
        }
    }
}


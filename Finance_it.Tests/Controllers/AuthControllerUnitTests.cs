using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Finance_it.API.Controllers;
using Finance_it.API.Dtos.ApiResponsesDtos;
using Finance_it.API.Dtos.UserDtos;
using Finance_it.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Finance_it.Tests.Controllers
{
    public class AuthControllerUnitTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly AuthController _authController;

        public AuthControllerUnitTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _authController = new AuthController(_mockAuthService.Object);
        }

        [Fact]
        public async Task Login_ReturnsOkResult_WhenLoginIsSuccessful()
        {
            // Arrange
            var loginDto = new LoginRequestDto { Email = "testuser", Password = "password123" };
            var expectedResponse = new ApiResponseDto<AuthenticationResponseDto>(200, new AuthenticationResponseDto
            {
                AccessToken = "fake-access-token",
                RefreshToken = "fake-refresh-token"
            });
            _mockAuthService.Setup(s => s.LoginAsync(loginDto)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _authController.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var apiResponse = Assert.IsType<ApiResponseDto<AuthenticationResponseDto>>(okResult.Value);

            Assert.Equal(200, apiResponse.StatusCode);
            Assert.Equal("fake-access-token", apiResponse.Data.AccessToken);
            Assert.Equal("fake-refresh-token", apiResponse.Data.RefreshToken);
        }

        [Fact]
        public async Task Login_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            var loginDto = new LoginRequestDto { Email = "testuser", Password = "password123" };
            _mockAuthService.Setup(s => s.LoginAsync(loginDto)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _authController.Login(loginDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);

            var apiResponse = Assert.IsType<ApiResponseDto<AuthenticationResponseDto>>(objectResult.Value);
            Assert.Equal(500, apiResponse.StatusCode);
            Assert.Contains("An error occurred while processing your request.", apiResponse.Errors);
            Assert.Contains("Database error", apiResponse.Errors);
        }

        [Fact]
        public async Task Logout_ReturnsOkResult_WhenLogoutIsSuccessful()
        {
            // Arrange
            var userId = 1;
            var expectedResponse = new ApiResponseDto<ConfirmationResponseDto>(200, new ConfirmationResponseDto
            {
                Message = "Logout successful"
            });
            _mockAuthService.Setup(s => s.LogoutAsync(userId)).ReturnsAsync(expectedResponse);

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            _authController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await _authController.Logout();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var apiResponse = Assert.IsType<ApiResponseDto<ConfirmationResponseDto>>(okResult.Value);

            Assert.Equal(200, apiResponse.StatusCode);
            Assert.Equal("Logout successful", apiResponse.Data.Message);
        }

        [Fact]
        public async Task Logout_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            var userId = 1;
            _mockAuthService.Setup(s => s.LogoutAsync(userId)).ThrowsAsync(new Exception("Database error"));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            _authController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await _authController.Logout();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);

            var apiResponse = Assert.IsType<ApiResponseDto<ConfirmationResponseDto>>(objectResult.Value);
            Assert.Equal(500, apiResponse.StatusCode);
            Assert.Contains("An error occurred while processing your request.", apiResponse.Errors);
            Assert.Contains("Database error", apiResponse.Errors);
        }
    }
}

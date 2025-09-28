using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance_it.API.Controllers;
using Finance_it.API.Dtos.ApiResponsesDtos;
using Finance_it.API.Dtos.UserDtos;
using Finance_it.API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Finance_it.Tests.Controllers
{
    public class UserControllerUnitTests
    {
        private readonly Mock<IUserService> _mockuserService;
        private readonly UserController _Controller;

        public UserControllerUnitTests()
        {
            _mockuserService = new Mock<IUserService>();
            _Controller = new UserController(_mockuserService.Object);
        }

        [Fact]
        public async Task RegisterUser_ReturnsOkResult_WhenUserIsCreated()
        {
            // Arrange
            var userDto = new RegisterRequestDto { Name = "User", Email = "testuser", Password = "password123" };
            var expectedResponse = new ApiResponseDto<AuthenticationResponseDto>(201, new AuthenticationResponseDto
            {
                AccessToken = "fake-access-token",
                RefreshToken = "fake-refresh-token"
            });


            _mockuserService.Setup(s => s.RegisterAsync(userDto)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _Controller.Register(userDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var apiResponse = Assert.IsType<ApiResponseDto<AuthenticationResponseDto>>(okResult.Value);

            Assert.Equal(201, apiResponse.StatusCode);
            Assert.Equal("fake-access-token", apiResponse.Data.AccessToken);
            Assert.Equal("fake-refresh-token", apiResponse.Data.RefreshToken);
        }

        [Fact]
        public async Task Register_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            var userDto = new RegisterRequestDto { Name = "User", Email = "testuser", Password = "password123" };
            _mockuserService.Setup(s => s.RegisterAsync(userDto)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _Controller.Register(userDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);

            var apiResponse = Assert.IsType<ApiResponseDto<AuthenticationResponseDto>>(objectResult.Value);
            Assert.Equal(500, apiResponse.StatusCode);
            Assert.Contains("An error occurred while processing your request.", apiResponse.Errors);
            Assert.Contains("Database error", apiResponse.Errors);
        }
    }
}

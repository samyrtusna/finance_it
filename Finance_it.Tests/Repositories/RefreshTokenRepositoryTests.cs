using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance_it.API.Models;
using Finance_it.API.Models.AppDbContext;
using Finance_it.API.Repositories.CustomRepositories;
using Microsoft.EntityFrameworkCore;

namespace Finance_it.Tests.Repositories
{
    public class RefreshTokenRepositoryTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                        .Options;
            var context = new AppDbContext(options);
            context.RefreshTokens.AddRange(
                new RefreshToken { Id = 1, Token = "token1", UserId = 1, ExpiresAt = DateTime.UtcNow.AddDays(7), IsRevoked = false },
                new RefreshToken { Id = 2, Token = "token2", UserId = 2, ExpiresAt = DateTime.UtcNow.AddDays(7), IsRevoked = false },
                new RefreshToken { Id = 3, Token = "token3", UserId = 3, ExpiresAt = DateTime.UtcNow.AddDays(7), IsRevoked = true }
                );
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task GetRefreshTokenByTokenAsync_ReturnsCorrectToken()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new RefreshTokenRepository(context);
            // Act
            var token = await repository.GetRefreshTokenByTokenAsync("token2");
            // Assert
            Assert.NotNull(token);
            Assert.Equal(2, token.UserId);
            // Act - Non-existing token
            var nonExistingToken = await repository.GetRefreshTokenByTokenAsync("nonexistent");
            // Assert
            Assert.Null(nonExistingToken);
        }
    }
}

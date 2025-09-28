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
    public class UserRepositoryTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                        .Options;

            var context = new AppDbContext(options);

            context.Users.AddRange(
                new User { Id = 1, Name = "samir", Email = "samir@gamil.com", Password = "samir2020", Role = (Role)1 },
                new User { Id = 2, Name = "mayas", Email = "mayas@gamil.com", Password = "mayas2020", Role = (Role)1 },
                new User { Id = 3, Name = "Damia", Email = "damia@gamil.com", Password = "damia2020", Role = (Role)1 }
                );
            context.SaveChanges();
            return context;
        }

        

        [Fact]
        public async Task GetUserByEmail_ReturnsCorrectUser()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new UserRepository(context);
            // Act
            var user = await repository.GetUserByEmailAsync("damia@gamil.com");
            // Assert
            Assert.NotNull(user);
            Assert.Equal("Damia", user.Name);

            // Act - Non-existing email
            var nonExistingUser = await repository.GetUserByEmailAsync("Mouloud@email.com");
            // Assert
            Assert.Null(nonExistingUser);
        }       
    }
}

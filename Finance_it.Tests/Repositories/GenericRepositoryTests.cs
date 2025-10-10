using System.Linq;
using System.Threading.Tasks;
using Finance_it.API.Data.AppDbContext;
using Finance_it.API.Data.Entities;
using Finance_it.API.Models;
using Finance_it.API.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Finance_it.Tests.Repositories
{
    public class GenericRepositoryTests
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
        public async Task GetAllUsers_ReturnsAllUsers()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new GenericRepository<User>(context);

            // Act
            var users = await repository.GetAllAsync();

            // Assert
            Assert.Equal(3, users.Count());
        }

        [Fact]
        public async Task GetUserById_ReturnsCorrectUserOrNull()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new GenericRepository<User>(context);

            // Act - Existing user
            var user = await repository.GetByIdAsync(2);

            // Assert
            Assert.NotNull(user);
            Assert.Equal("mayas", user.Name);

            // Act - Non-existing ID
            var nonExistingUser = await repository.GetByIdAsync(99);

            // Assert
            Assert.Null(nonExistingUser);
        }

        [Fact]
        public async Task AddUser_AddsUserSuccessfully()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new GenericRepository<User>(context);
            var newUser = new User { Id = 4, Name = "Melinda", Email = "melinda@gmail.com", Password = "melinda2020", Role = (Role)1 };

            // Act
            await repository.AddAsync(newUser);
            await repository.SaveAsync();

            var user = await repository.GetByIdAsync(4);

            // Assert
            Assert.NotNull(user);
            Assert.Equal("Melinda", user.Name);
        }

        [Fact]
        public async Task UpdateUser_UpdatesUserSuccessfully()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new GenericRepository<User>(context);
            var user = await repository.GetByIdAsync(1);

            // Act
            user.Name = "Samir Updated";
            repository.Update(user);
            await repository.SaveAsync();

            var updatedUser = await repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(updatedUser);
            Assert.Equal("Samir Updated", updatedUser.Name);
        }

        [Fact]
        public async Task DeleteUser_DeletesUserSuccessfully()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new GenericRepository<User>(context);
            var user = await repository.GetByIdAsync(3);

            // Act
            repository.Delete(user!);
            await repository.SaveAsync();

            var deletedUser = await repository.GetByIdAsync(3);

            // Assert
            Assert.Null(deletedUser);
        }

        [Fact]
        public async Task GetByFilterAsync_ReturnsCorrectUser()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new GenericRepository<User>(context);

            // Act
            var user = await repository.GetByFilterAsync(u => u.Email == "mayas@gamil.com");

            // Assert
            Assert.NotNull(user);
            Assert.Equal("mayas", user.Name);
        }

        [Fact]
        public async Task GetAllByFilterAsync_ReturnsCorrectUsers()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new GenericRepository<User>(context);

            // Act
            var users = await repository.GetAllByFilterAsync(u => u.Role == (Role)1);

            // Assert
            Assert.Equal(3, users.Count());
        }
    }
}

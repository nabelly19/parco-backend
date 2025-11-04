using ParcoBackend.Tests.TestHelpers;
using ParcoBackend.Model;
using ParcoBackend.Services;
using DTO;
using Xunit;

namespace ParcoBackend.Tests.Services
{
    public class VehicleServiceTests
    {
        [Fact]
        public async Task CreateAsync_Should_Add_New_Vehicle()
        {
            // Arrange
            var context = TestDbContextFactory.CreateContext();
            var service = new VehicleService(context);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Firstname = "Test",
                Lastname = "User",
                Email = "test@example.com",
                Phonenumber = "123456789",
                Password = "password123",
                Usercategory = "Motorist"
            };

            // Add user on test
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var dto = new VehicleCreateDto
            {
                VehiclePlate = "ABC1234",
                VehicleModel = "Carro de Teste",
                UserId = user.Id 
            };

            // Act
            await service.CreateAsync(dto);

            // Assert
            Assert.True(context.Vehicles.Any(v => v.Vehicleplate == "ABC1234"));

            TestDbContextFactory.Destroy(context);
        }
    }
}

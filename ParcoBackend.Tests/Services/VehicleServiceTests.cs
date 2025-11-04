using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using ParcoBackend.Model;
using ParcoBackend.Services;
using DTO;
using FluentAssertions;

namespace ParcoBackend.Tests.Services
{
    public class VehicleServiceTests
    {
        private ParcoDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ParcoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ParcoDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_Should_Add_New_Vehicle()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new VehicleService(context);

            var dto = new VehicleCreateDto
            {
                UserId = Guid.NewGuid(),
                VehicleModel = "Tesla Model 3",
                VehiclePlate = "ABC1D23"
            };

            // Act
            var result = await service.CreateAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.VehicleModel.Should().Be("Tesla Model 3");
            result.VehiclePlate.Should().Be("ABC1D23");
        }

        [Fact]
        public async Task CreateAsync_Should_Throw_When_Plate_Already_Exists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new VehicleService(context);

            var dto = new VehicleCreateDto
            {
                UserId = Guid.NewGuid(),
                VehicleModel = "Honda Civic",
                VehiclePlate = "XYZ9A88"
            };

            await service.CreateAsync(dto);

            // Act
            Func<Task> act = async () => await service.CreateAsync(dto);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Já existe um veículo com esta placa cadastrado.");
        }
    }
}

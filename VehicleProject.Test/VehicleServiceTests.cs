using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using VehicleProject.Data;
using VehicleProject.Models;
using VehicleProject.Services;

namespace VehicleProject.Test;

public class VehicleServiceTests
{
    private async Task<VehicleProjectContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<VehicleProjectContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            
            var context = new  VehicleProjectContext(options.Options);
            context.Database.EnsureCreated();
            if (!await context.Cars.AnyAsync())
            {
                for (int i = 0; i < 10; i++)
                {
                    context.Cars.Add(
                        new Car()
                        {
                            Engine = "v5",
                            Doors = 5,
                            Wheels = 4,
                            BodyType = "Sedan",
                            Horsepower = "50"
                        });
                }
                await context.SaveChangesAsync();
            }
            
            return  context;
        }
    [Fact]
    public async Task VehicleService_CreateCar_ReturnsCreatedCar()
    {
        //Arrange
        var car1 = new Car()
        {
            Engine = "v100",
            Doors = 5,
            Wheels = 4,
            BodyType = "Sedan",
            Horsepower = "50"
        };

        var dbcontext = await GetDbContext();
        var carRepo  = new CarService(dbcontext);
        //Act
        
        var result = await carRepo.CreateAsync(car1);
        
        //Assert
        result.Should().BeEquivalentTo(car1);
        
    }

    [Fact]
    public async Task VehicleService_GetCars_ReturnsListofCars()
    {
        //Arrange
        var dbcontext = await GetDbContext();
        var carRepo = new CarService(dbcontext);
        
        //Act
        var result = await carRepo.GetAllAsync();
        
        //Assert
        result.Should().BeAssignableTo<IEnumerable<Car>>();
        result.Count().Should().Be(10);
        result.Should().AllBeOfType<Car>();
        result.Should().Contain(c =>
            c.Engine == "v5" &&
            c.Doors == 5 &&
            c.Wheels == 4 &&
            c.BodyType == "Sedan"
        );

    }
    
    [Fact]
    public async Task VehicleService_GetCarById_ReturnsCar()
    {
        var dbcontext = await GetDbContext();
        var carRepo = new CarService(dbcontext);
        var expectedCar = dbcontext.Cars.First();
        
        var result = await carRepo.GetByIdAsync(expectedCar.Id);
        
        result.Should().BeEquivalentTo(expectedCar);
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<Car>();
    }
    
    // [Fact]
    // public async Task VehicleService_UpdateCar_ReturnsUpdatedCar()
    // {
    //     var dbcontext = await GetDbContext();
    //     var carRepo = new CarService(dbcontext);
    //     
    //     var existingCar = dbcontext.Cars.First();
    //     var updatedCar = new Car
    //     {
    //         Id = existingCar.Id,
    //         Engine = "v100",
    //         Doors = existingCar.Doors,
    //         Wheels = existingCar.Wheels,
    //         BodyType = existingCar.BodyType,
    //         Horsepower = existingCar.Horsepower
    //     };
    //     var result = await carRepo.UpdateAsync(existingCar.Id, updatedCar);
    //
    //     result.Should().BeTrue();
    //     
    //
    // }
    
    [Fact]
    public async Task VehicleService_DeleteCar_ReturnsTrue()
    {
        var dbcontext = await GetDbContext();
        var carRepo = new CarService(dbcontext);
        
        var existingCar = dbcontext.Cars.First();
        var result = await carRepo.DeleteAsync(existingCar.Id);
        
        result.Should().BeTrue();
    }
}

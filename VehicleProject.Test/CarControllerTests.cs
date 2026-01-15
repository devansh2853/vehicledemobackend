using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using VehicleProject.Controllers;
using VehicleProject.Models;
using VehicleProject.Services.Interfaces;

namespace VehicleProject.Test;

public class CarControllerTests
{
    private readonly ICarService _carService;
    public CarControllerTests()
    {
        _carService = A.Fake<ICarService>();
    }
    [Fact]
    public async Task CarController_GetCars_ReturnsListOfCars()
    {
        //Arrange
        var cars = A.Fake<List<Car>>();
        A.CallTo(() => _carService.GetAllAsync())
            .Returns(cars);
        var controller = new CarController(_carService);
        //Act
        var result = await controller.GetCars();
        //Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();
        // result.Should().BeOfType<OkObjectResult>();
        
    }

    [Fact]
    public async Task CarController_CreateCar_ReturnsCreatedCar()
    {
        //Arrange
         var car1 = A.Fake<Car>();
         A.CallTo(() => _carService.CreateAsync(car1)).Returns(car1);
         var controller = new CarController(_carService);
         
         //Act
         var result = await controller.CreateCar(car1);
         
         //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CreatedAtRouteResult>().Subject.Value.Should().Be(car1);
        // result.Should().BeEquivalentTo(car1);
    }
    
    [Fact]
    public async Task CarController_Get_ReturnsOkObjectResult()
    {
        var car1 = A.Fake<Car>();
        A.CallTo(()=>_carService.GetByIdAsync(car1.Id)).Returns(car1);
        var controller = new CarController(_carService);
        
        var result = await controller.Get(car1.Id);
        
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>()
            .Subject.Value.Should().Be(car1);
    
        // result.Value.Should().BeEquivalentTo(car1);
    }

    [Fact]
    public async Task CarController_UpdateCar_ReturnsNoContent()
    {
        var car1 = A.Fake<Car>();
        A.CallTo(() => _carService.UpdateAsync(car1.Id, car1)).Returns(UpdateResult.Success);
        var controller = new CarController(_carService);
        
        var result = await controller.UpdateCar(car1.Id, car1);
        
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task CarController_DeleteCar_ReturnsNoContent()
    {
        var car1 = A.Fake<Car>();
        A.CallTo(() => _carService.DeleteAsync(car1.Id)).Returns(true);
        var controller = new CarController(_carService);
        
        var result = await controller.DeleteCar(car1.Id);
        
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();
    }
}
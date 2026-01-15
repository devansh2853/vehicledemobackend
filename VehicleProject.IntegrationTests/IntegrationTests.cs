using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using VehicleProject.Models;
using Xunit.Abstractions;

namespace VehicleProject.IntegrationTests;

public class IntegrationTests : IClassFixture<VehicleProjectWebApplicationFactory>
{
    private readonly VehicleProjectWebApplicationFactory _factory;
    private readonly ITestOutputHelper _output;
    public IntegrationTests(VehicleProjectWebApplicationFactory factory, ITestOutputHelper output)
    {
        _output = output;
        _factory = factory;
    }

    [Fact]
    public async Task CarController_GetCars_ReturnsOk()
    {
        var client = _factory.CreateClient();
        
        var response = await client.GetAsync("/Car");
        
        var statusCode = response.StatusCode;
        var headers = response.Headers;
       
        var body = await response.Content.ReadAsStringAsync();

        _output.WriteLine($"Status: {response.StatusCode}");
        _output.WriteLine($"Body: {body}");
        
        response.EnsureSuccessStatusCode();
        
    }
    
    [Fact]
    public async Task CarController_AddCar_ReturnsOk()
    {
        var client = _factory.CreateClient();
        var car = new Car { Wheels = 4, BodyType = "Sedan"};
        var response = await client.PostAsJsonAsync("/Car", car);
        var statusCode = response.StatusCode;
        
        var headers = response.Headers;
        var body = await response.Content.ReadAsStringAsync();
        
        _output.WriteLine($"Status: {response.StatusCode}");
        _output.WriteLine($"Body: {body}");
        response.EnsureSuccessStatusCode();
        
    }
    
    
}
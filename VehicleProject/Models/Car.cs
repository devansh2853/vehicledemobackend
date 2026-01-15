namespace VehicleProject.Models;

public class Car : Vehicle
{
    public string? Engine {get; set;}
    public int Doors {get; set;}
    public int Wheels {get; set;}
    public string? BodyType {get; set;}
    public string? Horsepower {get; set;}
    public Car()
    {
        // VehicleType = "Car";
    }
}
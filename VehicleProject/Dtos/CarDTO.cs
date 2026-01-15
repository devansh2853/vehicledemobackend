namespace VehicleProject.Dtos;

public class CarDTO : VehicleDto
{
    public string? Engine {get; set;}
    public int Doors {get; set;}
    public int Wheels {get; set;}
    public string? BodyType {get; set;}
    public string? Horsepower {get; set;}
}
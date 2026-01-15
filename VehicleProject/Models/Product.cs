using System;

namespace VehicleProject.Models;

public class Product
{
    public int Id { get; set; }
    public required string ProductName { get; set; }
    public string? ProductDescription {get; set;}
}

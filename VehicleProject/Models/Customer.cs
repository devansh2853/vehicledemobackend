using System;

namespace VehicleProject.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PhoneNumber { get; set; }
    public List<Order> Order {get; set;} = new List<Order>();

}

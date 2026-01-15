using System;

namespace VehicleProject.Models;

public class Order
{
    public int Id { get; set; }
    public List<Product> Products {get; set;} = new List<Product>();
    public int CustomerId {get; set;}
    public required Customer Customer;
}

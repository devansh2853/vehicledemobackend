using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using VehicleProject.Models;

namespace VehicleProject.Data;

public class VehicleProjectContext : DbContext
{
    public VehicleProjectContext(DbContextOptions<VehicleProjectContext> options) : base(options) {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VehicleType>().HasData(
            new VehicleType {Id = 1, Name = "Car"},
            new VehicleType {Id = 2, Name = "Bus"}
        );
    }
    public DbSet<Car> Cars {get; set;}
    // public DbSet<Car> Cars => Set<Car>();
    public DbSet<VehicleType> VehicleTypes {get; set;}

}
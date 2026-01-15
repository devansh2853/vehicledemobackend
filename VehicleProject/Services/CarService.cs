using Microsoft.EntityFrameworkCore;
using VehicleProject.Data;
using VehicleProject.Models;
using VehicleProject.Services.Interfaces;

namespace VehicleProject.Services;

public class CarService : ICarService
{
    private readonly VehicleProjectContext _context;

    public CarService(VehicleProjectContext context)
    {
        _context = context;
    }

    public async Task<List<Car>> GetAllAsync()
    {
        return await _context.Cars.ToListAsync();
    }

    public async Task<Car?> GetByIdAsync(int id)
    {
        return await _context.Cars.FindAsync(id);
    }
    
    public async Task<Car> CreateAsync(Car car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
        return car;
    }
    
    public async Task<UpdateResult> UpdateAsync(int id, Car car)
    {
        if (id != car.Id) return UpdateResult.IdMismatch;
        if (!await _context.Cars.AnyAsync(c => c.Id == id)) return UpdateResult.NotFound;

        _context.Cars.Update(car);
        await _context.SaveChangesAsync();
        return UpdateResult.Success;
    }
    
    public async Task<bool> DeleteAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null) return false;

        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
        return true;
    }
}
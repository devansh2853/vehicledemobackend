using VehicleProject.Models;

namespace VehicleProject.Services.Interfaces;

public interface ICarService
{
    Task<List<Car>> GetAllAsync();
    Task<Car?> GetByIdAsync(int id);
    Task<Car> CreateAsync(Car car);
    Task<UpdateResult> UpdateAsync(int id, Car car);
    Task<bool> DeleteAsync(int id);
}
using CarManufacturingIndustryManagement.Models;

namespace CarManufacturingIndustryManagement.Services
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAllCarModelsAsync();
        Task<IEnumerable<Car>> GetCarModelByQueryAsync(string query);
        Task<Car> AddCarModelAsync(Car car);
        Task<bool> UpdateCarModelAsync(int id, Car updatedCar);
        Task<bool> DeleteCarModelAsync(int id);
    }
}

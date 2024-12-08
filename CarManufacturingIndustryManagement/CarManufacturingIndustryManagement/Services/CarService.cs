using CarManufacturingIndustryManagement.Data;
using CarManufacturingIndustryManagement.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CarManufacturingIndustryManagement.Services
{
    public class CarService : ICarService
    {
        private readonly AppDbContext _context;

        public CarService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAllCarModelsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetCarModelByQueryAsync(string query)
        {
            bool isId = int.TryParse(query, out int modelId);

            var cars = await _context.Cars
                .Where(c =>
                    c.ModelName.ToLower().Contains(query.ToLower()) ||  // Case-insensitive match for ModelName
                    c.Status.ToLower().Contains(query.ToLower()) ||     // Case-insensitive match for Status
                    (isId && c.ModelId == modelId) ||                  // Match by ModelId if query is a number
                    (isId && c.FuelEfficiency == modelId))             // Match by FuelEfficiency if query is a number
                .ToListAsync();

            return cars; // Return the list of Car entities
        }


        public async Task<Car> AddCarModelAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<bool> UpdateCarModelAsync(int id, Car updatedCar)
        {
            var existingCar = await _context.Cars.FindAsync(id);
            if (existingCar == null) return false;

            existingCar.ModelName = updatedCar.ModelName;
            existingCar.EngineType = updatedCar.EngineType;
            existingCar.FuelEfficiency = updatedCar.FuelEfficiency;
            existingCar.DesignFeatures = updatedCar.DesignFeatures;
            existingCar.ProductionCost = updatedCar.ProductionCost;
            existingCar.Status = updatedCar.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCarModelAsync(int id)
        {
            var carModel = await _context.Cars.FindAsync(id);
            if (carModel == null) return false;

            _context.Cars.Remove(carModel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

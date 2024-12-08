using CarManufacturingIndustryManagement.Data;
using CarManufacturingIndustryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CarManufacturingIndustryManagement.Services
{
    public class ProductionOrderService : IProductionOrderService
    {
        private readonly AppDbContext _context;

        public ProductionOrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductionOrder>> GetAllProductionOrdersAsync()
        {
            return await _context.ProductionOrders.ToListAsync();
        }

        public async Task<ProductionOrder> GetProductionOrderByIdAsync(int orderId)
        {
            return await _context.ProductionOrders.FindAsync(orderId);
        }

        public async Task<ProductionOrder> AddProductionOrderAsync(ProductionOrder productionOrder)
        {
            // Validate CarModelId
            var carExists = await _context.Cars.AnyAsync(c => c.ModelId == productionOrder.CarModelId);
            if (!carExists)
            {
                throw new Exception($"Car model with ID {productionOrder.CarModelId} does not exist.");
            }

            // Validate ProductionManagerId with role as Manager
            var managerExists = await _context.Users
                .AnyAsync(u => u.UserId == productionOrder.ProductionManagerId && u.Role == "Manager");
            if (!managerExists)
            {
                throw new Exception($"Production Manager with ID {productionOrder.ProductionManagerId} does not exist or is not a Manager.");
            }

            // Add and save the production order
            await _context.ProductionOrders.AddAsync(productionOrder);
            await _context.SaveChangesAsync();
            return productionOrder;
        }

        public async Task<bool> UpdateProductionOrderAsync(int orderId, ProductionOrder updatedOrder)
        {
            var existingOrder = await _context.ProductionOrders.FindAsync(orderId);
            if (existingOrder == null) return false;

            var carExists = await _context.Cars.AnyAsync(c => c.ModelId == updatedOrder.CarModelId);
            if (!carExists)
            {
                throw new Exception($"Car model with ID {updatedOrder.CarModelId} does not exist.");
            }

            var managerExists = await _context.Users
                .AnyAsync(u => u.UserId == updatedOrder.ProductionManagerId && u.Role == "Manager");
            if (!managerExists)
            {
                throw new Exception($"Production Manager with ID {updatedOrder.ProductionManagerId} does not exist or is not a Manager.");
            }

            // Update the order properties
            existingOrder.CarModelId = updatedOrder.CarModelId;
            existingOrder.StartDate = updatedOrder.StartDate;
            existingOrder.EndDate = updatedOrder.EndDate;
            existingOrder.Quantity = updatedOrder.Quantity;
            existingOrder.ProductionManagerId = updatedOrder.ProductionManagerId;
            existingOrder.Status = updatedOrder.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductionOrderAsync(int orderId)
        {
            var order = await _context.ProductionOrders.FindAsync(orderId);
            if (order == null) return false;

            _context.ProductionOrders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

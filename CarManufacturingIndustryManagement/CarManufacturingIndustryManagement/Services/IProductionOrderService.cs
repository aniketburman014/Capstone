using CarManufacturingIndustryManagement.Models;

namespace CarManufacturingIndustryManagement.Services
{
    public interface IProductionOrderService
    {
        Task<IEnumerable<ProductionOrder>> GetAllProductionOrdersAsync();
        Task<ProductionOrder> GetProductionOrderByIdAsync(int orderId);
        Task<ProductionOrder> AddProductionOrderAsync(ProductionOrder productionOrder);
        Task<bool> UpdateProductionOrderAsync(int orderId, ProductionOrder updatedOrder);
        Task<bool> DeleteProductionOrderAsync(int orderId);
    }
}

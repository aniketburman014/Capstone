using CarManufacturingIndustryManagement.Models;
using System.Threading.Tasks;

namespace CarManufacturingIndustryManagement.Services
{
    public interface IInventoryService
    {
        Task<List<Inventory>> GetAllInventoriesAsync();
        Task<IEnumerable<Inventory>> SearchInventoryAsync(string searchQuery);
        Task<Inventory> AddInventoryAsync(Inventory inventory);
        Task<bool> UpdateInventoryAsync(int inventoryId, Inventory updatedInventory);
        Task<bool> DeleteInventoryAsync(int inventoryId);
    }
}

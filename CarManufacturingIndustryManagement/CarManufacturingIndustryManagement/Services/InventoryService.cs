using CarManufacturingIndustryManagement.Data;
using CarManufacturingIndustryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CarManufacturingIndustryManagement.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _context;

        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Inventory>> GetAllInventoriesAsync()
        {
            return await _context.Inventories.ToListAsync();
        }

        public async Task<IEnumerable<Inventory>> SearchInventoryAsync(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                // Return all inventories if no search query is provided
                return await _context.Inventories.ToListAsync();
            }

            // Convert the search query to lowercase for case-insensitive search
            searchQuery = searchQuery.ToLower();

            return await _context.Inventories
                .Where(inventory =>
                    inventory.InventoryId.ToString().Contains(searchQuery) ||  // Match by InventoryId
                    (inventory.ComponentName != null && inventory.ComponentName.ToLower().Contains(searchQuery)) || // Match by ComponentName
                    inventory.Quantity.ToString().Contains(searchQuery) ||      // Match by Quantity
                    inventory.StockLevel.ToString().Contains(searchQuery) ||    // Match by StockLevel
                    inventory.ReorderThreshold.ToString().Contains(searchQuery) // Match by ReorderThreshold
                )
                .ToListAsync();
        }

        public async Task<Inventory> AddInventoryAsync(Inventory inventory)
        {
            var supplierExists = await _context.Suppliers.AnyAsync(s => s.SupplierId == inventory.SupplierId);
            if (!supplierExists)
            {
                throw new Exception("Supplier does not exist.");
            }

            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        public async Task<bool> UpdateInventoryAsync(int inventoryId, Inventory updatedInventory)
        {
            var existingInventory = await _context.Inventories.FindAsync(inventoryId);
            if (existingInventory == null)
            {
                return false;
            }

            var supplierExists = await _context.Suppliers.AnyAsync(s => s.SupplierId == updatedInventory.SupplierId);
            if (!supplierExists)
            {
                throw new Exception("Supplier does not exist.");
            }

            // Update properties
            existingInventory.ComponentName = updatedInventory.ComponentName;
            existingInventory.Quantity = updatedInventory.Quantity;
            existingInventory.SupplierId = updatedInventory.SupplierId;
            existingInventory.StockLevel = updatedInventory.StockLevel;
            existingInventory.ReorderThreshold = updatedInventory.ReorderThreshold;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteInventoryAsync(int inventoryId)
        {
            var inventory = await _context.Inventories.FindAsync(inventoryId);
            if (inventory == null)
            {
                return false;
            }

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

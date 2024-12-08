using CarManufacturingIndustryManagement.Data;
using CarManufacturingIndustryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CarManufacturingIndustryManagement.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly AppDbContext _context;

        public SupplierService(AppDbContext context)
        {
            _context = context;
        }

        // Get all suppliers
        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        // Search suppliers based on multiple fields
        public async Task<IEnumerable<Supplier>> SearchSuppliersAsync(string searchQuery)
        {
            searchQuery = searchQuery.ToLower();
            var result = await _context.Suppliers
    .Where(supplier =>
        supplier.SupplierId.ToString().Contains(searchQuery) ||
        supplier.Name.ToLower().Contains(searchQuery) ||
        supplier.ContactDetails.ToLower().Contains(searchQuery) ||
        supplier.MaterialType.ToLower().Contains(searchQuery) ||
        supplier.Status.ToLower().Contains(searchQuery))
    .ToListAsync();

            Console.WriteLine($"Found {result.Count} suppliers.");
            return result;
        }

        // Create a new supplier
        public async Task<Supplier> CreateSupplierAsync(Supplier supplier)
        {
            if (supplier == null)
            {
                throw new ArgumentNullException(nameof(supplier), "Supplier cannot be null");
            }

            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        // Update an existing supplier
        public async Task<bool> UpdateSupplierAsync(int id, Supplier supplier)
        {
            if (supplier == null)
            {
                throw new ArgumentNullException(nameof(supplier), "Supplier data cannot be null");
            }

            var existingSupplier = await _context.Suppliers.FindAsync(id);
            if (existingSupplier == null) return false;

            // Check if any fields have changed before updating
            bool isUpdated = false;

            if (existingSupplier.Name != supplier.Name)
            {
                existingSupplier.Name = supplier.Name;
                isUpdated = true;
            }

            if (existingSupplier.ContactDetails != supplier.ContactDetails)
            {
                existingSupplier.ContactDetails = supplier.ContactDetails;
                isUpdated = true;
            }

            if (existingSupplier.MaterialType != supplier.MaterialType)
            {
                existingSupplier.MaterialType = supplier.MaterialType;
                isUpdated = true;
            }

            if (existingSupplier.DeliveryTime != supplier.DeliveryTime)
            {
                existingSupplier.DeliveryTime = supplier.DeliveryTime;
                isUpdated = true;
            }

            if (existingSupplier.Pricing != supplier.Pricing)
            {
                existingSupplier.Pricing = supplier.Pricing;
                isUpdated = true;
            }

            if (existingSupplier.Status != supplier.Status)
            {
                existingSupplier.Status = supplier.Status;
                isUpdated = true;
            }

            // Only update if changes were detected
            if (isUpdated)
            {
                _context.Suppliers.Update(existingSupplier);
                await _context.SaveChangesAsync();
            }

            return isUpdated;
        }

        // Delete a supplier by id
        public async Task<bool> DeleteSupplierAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null) return false;

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

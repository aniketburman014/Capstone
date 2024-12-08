using CarManufacturingIndustryManagement.Models;

namespace CarManufacturingIndustryManagement.Services
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetSuppliersAsync();
        Task<IEnumerable<Supplier>> SearchSuppliersAsync(string query);
        Task<Supplier> CreateSupplierAsync(Supplier supplier);
        Task<bool> UpdateSupplierAsync(int id, Supplier supplier);
        Task<bool> DeleteSupplierAsync(int id);
    }
}

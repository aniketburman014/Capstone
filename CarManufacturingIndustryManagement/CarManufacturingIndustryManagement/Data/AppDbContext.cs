using Microsoft.EntityFrameworkCore;
using CarManufacturingIndustryManagement.Models;
namespace CarManufacturingIndustryManagement.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<ProductionOrder> ProductionOrders { get; set; }
        public DbSet<QualityReport> QualityReports { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}

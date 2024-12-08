using CarManufacturingIndustryManagement.Models;
using CarManufacturingIndustryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarManufacturingIndustryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // Get all suppliers
        [HttpGet]
        [Authorize(Roles = "Admin,Inventory Manager")]
        public async Task<IActionResult> GetSuppliers()
        {
            var suppliers = await _supplierService.GetSuppliersAsync();
            if (suppliers == null || !suppliers.Any())
            {
                return NotFound(new { Message = "No suppliers found." });
            }
            return Ok(suppliers);
        }

        // Search for suppliers by query
        [HttpGet("search/{query}")]
        [Authorize(Roles = "Admin,Inventory Manager")]
        public async Task<IActionResult> SearchSuppliers(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { Message = "Search query cannot be empty." });
            }

            var suppliers = await _supplierService.SearchSuppliersAsync(query);
            if (suppliers == null || !suppliers.Any())
            {
                return NotFound(new { Message = "No suppliers matching the search criteria were found." });
            }
            return Ok(suppliers);
        }

        // Create a new supplier
        [HttpPost]
        [Authorize(Roles = "Admin,Inventory Manager")]
        public async Task<IActionResult> CreateSupplier([FromBody] Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdSupplier = await _supplierService.CreateSupplierAsync(supplier);
            return CreatedAtAction(nameof(GetSupplier), new { id = createdSupplier.SupplierId }, createdSupplier);
        }

        // Get a supplier by id (single supplier)
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Inventory Manager")]
        public async Task<IActionResult> GetSupplier(int id)
        {
            var supplier = await _supplierService.GetSuppliersAsync();
            var foundSupplier = supplier.FirstOrDefault(s => s.SupplierId == id);
            if (foundSupplier == null)
            {
                return NotFound(new { Message = "Supplier not found" });
            }
            return Ok(foundSupplier);
        }

        // Update an existing supplier
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Inventory Manager")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = await _supplierService.UpdateSupplierAsync(id, supplier);
            if (!isUpdated)
            {
                return NotFound(new { Message = "Supplier not found" });
            }

            return NoContent();
        }

        // Delete a supplier by id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Inventory Manager")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var isDeleted = await _supplierService.DeleteSupplierAsync(id);
            if (!isDeleted)
            {
                return NotFound(new { Message = "Supplier not found" });
            }

            return NoContent();
        }
    }
}

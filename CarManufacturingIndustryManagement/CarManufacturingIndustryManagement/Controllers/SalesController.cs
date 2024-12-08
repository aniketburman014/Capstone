using CarManufacturingIndustryManagement.Data;
using CarManufacturingIndustryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarManufacturingIndustryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Sales
        [HttpGet]
        [Authorize(Roles = "Admin,Sales Manager")]
        public async Task<IActionResult> GetSalesOrders()
        {
            var salesOrders = await _context.SalesOrders
                .Select(so => new
                {
                    so.OrderId,
                    so.CustomerId,
                    so.CarModelId,
                    so.OrderDate,
                    so.DeliveryDate,
                    so.Price,
                    so.Status
                })
                .ToListAsync();

            return Ok(salesOrders);
        }

        // GET: api/Sales/search?query={query}
        [HttpGet("search")]
        [Authorize(Roles = "Admin,Sales Manager")]
        public async Task<IActionResult> SearchSalesOrders([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { Message = "Search query cannot be empty" });
            }

            var searchResults = await _context.SalesOrders
                .Where(so => EF.Functions.Like(so.CustomerId.ToString(), $"%{query}%") ||
                             EF.Functions.Like(so.CarModelId.ToString(), $"%{query}%") ||
                             EF.Functions.Like(so.Status, $"%{query}%") ||
                             EF.Functions.Like(so.Price.ToString(), $"%{query}%"))
                .ToListAsync();

            

            return Ok(searchResults);
        }


        // GET: api/Sales/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Sales Manager")]
        public async Task<IActionResult> GetSalesOrderById(int id)
        {
            var salesOrder = await _context.SalesOrders
                .Where(so => so.OrderId == id)
                .Select(so => new
                {
                    so.OrderId,
                    so.CustomerId,
                    so.CarModelId,
                    so.OrderDate,
                    so.DeliveryDate,
                    so.Price,
                    so.Status
                })
                .FirstOrDefaultAsync();

            if (salesOrder == null)
            {
                return NotFound(new { Message = "Sales Order not found" });
            }

            return Ok(salesOrder);
        }

        // POST: api/Sales
        [HttpPost]
        [Authorize(Roles = "Admin,Sales Manager")]
        public async Task<IActionResult> CreateSalesOrder([FromBody] SalesOrder salesOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var carExists = await _context.Cars.AnyAsync(c => c.ModelId == salesOrder.CarModelId);

            if (!carExists)
            {
                return BadRequest(new { Message = "Invalid Car Model ID" });
            }

            await _context.SalesOrders.AddAsync(salesOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSalesOrderById), new { id = salesOrder.OrderId }, salesOrder);
        }

        // PUT: api/Sales/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Sales Manager")]
        public async Task<IActionResult> UpdateSalesOrder(int id, [FromBody] SalesOrder salesOrder)
        {
            if (id != salesOrder.OrderId || !ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid request data" });
            }

            var existingSalesOrder = await _context.SalesOrders.FindAsync(id);
            if (existingSalesOrder == null)
            {
                return NotFound(new { Message = "Sales Order not found" });
            }

            // Validate foreign keys
            var customerExists = await _context.Customers.AnyAsync(c => c.CustomerId == salesOrder.CustomerId);
            var carExists = await _context.Cars.AnyAsync(c => c.ModelId == salesOrder.CarModelId);

            if (!customerExists || !carExists)
            {
                return BadRequest(new { Message = "Invalid Customer ID or Car Model ID" });
            }

            existingSalesOrder.CustomerId = salesOrder.CustomerId;
            existingSalesOrder.CarModelId = salesOrder.CarModelId;
            existingSalesOrder.OrderDate = salesOrder.OrderDate;
            existingSalesOrder.DeliveryDate = salesOrder.DeliveryDate;
            existingSalesOrder.Price = salesOrder.Price;
            existingSalesOrder.Status = salesOrder.Status;

            _context.SalesOrders.Update(existingSalesOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Sales/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Sales Manager")]
        public async Task<IActionResult> DeleteSalesOrder(int id)
        {
            var salesOrder = await _context.SalesOrders.FindAsync(id);
            if (salesOrder == null)
            {
                return NotFound(new { Message = "Sales Order not found" });
            }

            _context.SalesOrders.Remove(salesOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

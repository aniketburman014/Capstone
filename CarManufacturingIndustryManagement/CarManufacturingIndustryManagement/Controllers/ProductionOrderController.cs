using CarManufacturingIndustryManagement.Models;
using CarManufacturingIndustryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarManufacturingIndustryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionOrderController : ControllerBase
    {
        private readonly IProductionOrderService _productionOrderService;

        public ProductionOrderController(IProductionOrderService productionOrderService)
        {
            _productionOrderService = productionOrderService;
        }

        // GET: api/ProductionOrder
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetAllProductionOrders()
        {
            var orders = await _productionOrderService.GetAllProductionOrdersAsync();
            return Ok(orders);
        }

        // GET: api/ProductionOrder/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetProductionOrder(int id)
        {
            var order = await _productionOrderService.GetProductionOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound(new { message = "Production order not found." });
            }

            return Ok(order);
        }

        // POST: api/ProductionOrder
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> AddProductionOrder([FromBody] ProductionOrder productionOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdOrder = await _productionOrderService.AddProductionOrderAsync(productionOrder);
                return CreatedAtAction(nameof(GetProductionOrder), new { id = createdOrder.OrderId }, createdOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/ProductionOrder/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateProductionOrder(int id, [FromBody] ProductionOrder updatedOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var isUpdated = await _productionOrderService.UpdateProductionOrderAsync(id, updatedOrder);
                if (!isUpdated)
                {
                    return NotFound(new { message = "Production order not found." });
                }

                return Ok(new { message = "Production order updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/ProductionOrder/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteProductionOrder(int id)
        {
            var isDeleted = await _productionOrderService.DeleteProductionOrderAsync(id);
            if (!isDeleted)
            {
                return NotFound(new { message = "Production order not found." });
            }

            return Ok(new { message = "Production order deleted successfully." });
        }
    }
}

using CarManufacturingIndustryManagement.Models;
using CarManufacturingIndustryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarManufacturingIndustryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // GET: api/Inventory
        [HttpGet]
        [Authorize(Roles = "Admin,Inventory Manager")]
        public async Task<IActionResult> GetAllInventories()
        {
            var inventories = await _inventoryService.GetAllInventoriesAsync();
            return Ok(inventories);
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin,Inventory Manager")]
        public async Task<IActionResult> SearchInventory([FromQuery] string query)
        {
            // If query is null or empty, return all inventory records
            var inventories = await _inventoryService.SearchInventoryAsync(query);

            if (inventories == null || !inventories.Any())
            {
                return NotFound(new { message = "No inventory items match the search criteria." });
            }

            return Ok(inventories);
        }

        // POST: api/Inventory
        [HttpPost]
        [Authorize(Roles = "Admin,Inventory Manager")]
        public async Task<IActionResult> AddInventory([FromBody] Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdInventory = await _inventoryService.AddInventoryAsync(inventory);
                return CreatedAtAction(nameof(SearchInventory), new { id = createdInventory.InventoryId }, createdInventory);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Inventory/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Inventory Manager")]
        public async Task<IActionResult> UpdateInventory(int id, [FromBody] Inventory updatedInventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var isUpdated = await _inventoryService.UpdateInventoryAsync(id, updatedInventory);
                if (!isUpdated)
                {
                    return NotFound(new { message = "Inventory item not found." });
                }

                return Ok(new { message = "Inventory updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Inventory/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Inventory Manager")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var isDeleted = await _inventoryService.DeleteInventoryAsync(id);
            if (!isDeleted)
            {
                return NotFound(new { message = "Inventory item not found." });
            }

            return Ok(new { message = "Inventory deleted successfully." });
        }
    }
}

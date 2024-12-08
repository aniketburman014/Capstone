using CarManufacturingIndustryManagement.Models;
using CarManufacturingIndustryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarManufacturingIndustryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("AllCarModel")]
        [Authorize(Roles = "Admin,Engineer")]
        public async Task<IActionResult> GetAllCarModels()
        {
            var carModels = await _carService.GetAllCarModelsAsync();
            return Ok(carModels);
        }

        [HttpGet("SearchCarModel")]
        [Authorize(Roles = "Admin,Engineer")]
        public async Task<IActionResult> GetCarModel([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { message = "Query parameter cannot be empty." });
            }

            var carModels = await _carService.GetCarModelByQueryAsync(query);

            if (carModels == null || !carModels.Any())
            {
                return NotFound(new { message = "No car models found matching the query." });
            }

            return Ok(carModels);
        }


        [HttpPost("AddCarModel")]
        [Authorize(Roles = "Admin,Engineer")]
        public async Task<IActionResult> AddCarModel([FromBody] Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCar = await _carService.AddCarModelAsync(car);
            return CreatedAtAction(nameof(GetCarModel), new { id = createdCar.ModelId }, createdCar);
        }

        [HttpPut("UpdateCarModel/{id}")]
        [Authorize(Roles = "Admin,Engineer")]
        public async Task<IActionResult> UpdateCarModel(int id, [FromBody] Car updatedCar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = await _carService.UpdateCarModelAsync(id, updatedCar);
            if (!isUpdated)
            {
                return NotFound(new { message = "Car model not found." });
            }

            return Ok(new { message = "Car model updated successfully." });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var isDeleted = await _carService.DeleteCarModelAsync(id);
            if (!isDeleted)
            {
                return NotFound(new { message = "Car model not found." });
            }

            return Ok(new { message = "Car model deleted successfully." });
        }
    }
}

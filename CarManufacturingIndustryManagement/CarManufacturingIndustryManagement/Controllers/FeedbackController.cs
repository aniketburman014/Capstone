using CarManufacturingIndustryManagement.Models;
using CarManufacturingIndustryManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarManufacturingIndustryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _service;

        public FeedbackController(IFeedbackService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("search")]
        public IActionResult GetByQuery([FromQuery] string query)
        {
            return Ok(_service.GetByQuery(query));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Feedback feedback)
        {
            if (feedback == null)
            {
                return BadRequest("Feedback is null.");
            }

            var createdFeedback = _service.Create(feedback);
            return CreatedAtAction(nameof(Create), new { id = createdFeedback.Id }, createdFeedback);
        }
    }
}

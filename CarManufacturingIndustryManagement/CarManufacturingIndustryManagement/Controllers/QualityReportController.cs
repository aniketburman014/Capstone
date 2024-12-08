using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CarManufacturingIndustryManagement.Models;
using CarManufacturingIndustryManagement.Services;
using System.Threading.Tasks;

namespace CarManufacturingIndustryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualityReportController : ControllerBase
    {
        private readonly IQualityReportService _qualityReportService;

        public QualityReportController(IQualityReportService qualityReportService)
        {
            _qualityReportService = qualityReportService;
        }

        // Add a new quality report
        [HttpPost("add")]
        [Authorize(Roles = "Admin,QualityInspector")]
        public async Task<IActionResult> AddReport([FromBody] QualityReport report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdReport = await _qualityReportService.AddReportAsync(report);
                return CreatedAtAction(nameof(GetReportById), new { id = createdReport.ReportId }, createdReport);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Get all quality reports
        [HttpGet("all")]
        [Authorize(Roles = "Admin,QualityInspector")]
        public async Task<IActionResult> GetAllReports()
        {
            try
            {
                var reports = await _qualityReportService.GetAllReportsAsync();
                return Ok(reports);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Get a specific quality report by ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,QualityInspector")]
        public async Task<IActionResult> GetReportById(int id)
        {
            try
            {
                var report = await _qualityReportService.GetReportByIdAsync(id);
                if (report == null)
                {
                    return NotFound(new { message = "Quality report not found." });
                }

                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Update a specific quality report
        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin,QualityInspector")]
        public async Task<IActionResult> UpdateReport(int id, [FromBody] QualityReport updatedReport)
        {
            if (id != updatedReport.ReportId)
            {
                return BadRequest(new { message = "ID in request does not match report ID." });
            }

            try
            {
                var success = await _qualityReportService.UpdateReportAsync(id, updatedReport);
                if (!success)
                {
                    return NotFound(new { message = "Quality report not found." });
                }

                return Ok(new { message = "Quality report updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Delete a specific quality report by ID
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin,QualityInspector")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            try
            {
                var success = await _qualityReportService.DeleteReportAsync(id);
                if (!success)
                {
                    return NotFound(new { message = "Quality report not found." });
                }

                return Ok(new { message = "Quality report deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

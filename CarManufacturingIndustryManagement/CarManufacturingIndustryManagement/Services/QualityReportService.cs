using CarManufacturingIndustryManagement.Data;
using CarManufacturingIndustryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CarManufacturingIndustryManagement.Services
{
    public class QualityReportService : IQualityReportService
    {
        private readonly AppDbContext _context;

        public QualityReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<QualityReport>> GetAllReportsAsync()
        {
            return await _context.QualityReports.ToListAsync();
        }

        public async Task<QualityReport> GetReportByIdAsync(int reportId)
        {
            return await _context.QualityReports.FirstAsync(r => r.ReportId == reportId);
        }

        public async Task<QualityReport> AddReportAsync(QualityReport report)
        {
            // Validate CarModelId
            var carExists = await _context.Cars.AnyAsync(c => c.ModelId == report.CarModelId);
            if (!carExists)
            {
                throw new Exception("Invalid CarModelId. The specified car model does not exist.");
            }

            // Validate InspectorId and Role
            var inspector = await _context.Users.FirstOrDefaultAsync(u => u.UserId == report.InspectorId && u.Role == "QualityInspector");
            if (inspector == null)
            {
                throw new Exception("Invalid InspectorId. The user does not exist or is not a Quality Inspector.");
            }

            _context.QualityReports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<bool> UpdateReportAsync(int reportId, QualityReport updatedReport)
        {
            var existingReport = await _context.QualityReports.FindAsync(reportId);
            if (existingReport == null) return false;

            // Validate CarModelId
            var carExists = await _context.Cars.AnyAsync(c => c.ModelId == updatedReport.CarModelId);
            if (!carExists)
            {
                throw new Exception("Invalid CarModelId. The specified car model does not exist.");
            }

            // Validate InspectorId and Role
            var inspector = await _context.Users.FirstOrDefaultAsync(u => u.UserId == updatedReport.InspectorId && u.Role == "QualityInspector");
            if (inspector == null)
            {
                throw new Exception("Invalid InspectorId. The user does not exist or is not a Quality Inspector.");
            }

            existingReport.InspectionDate = updatedReport.InspectionDate;
            existingReport.TestResults = updatedReport.TestResults;
            existingReport.DefectsFound = updatedReport.DefectsFound;
            existingReport.Status = updatedReport.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteReportAsync(int reportId)
        {
            var report = await _context.QualityReports.FindAsync(reportId);
            if (report == null) return false;

            _context.QualityReports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

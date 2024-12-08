using CarManufacturingIndustryManagement.Models;

namespace CarManufacturingIndustryManagement.Services
{
    public interface IQualityReportService
    {
        Task<List<QualityReport>> GetAllReportsAsync();
        Task<QualityReport> GetReportByIdAsync(int reportId);
        Task<QualityReport> AddReportAsync(QualityReport report);
        Task<bool> UpdateReportAsync(int reportId, QualityReport updatedReport);
        Task<bool> DeleteReportAsync(int reportId);
    }
}

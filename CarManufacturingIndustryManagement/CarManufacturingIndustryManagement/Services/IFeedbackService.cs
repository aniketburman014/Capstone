using CarManufacturingIndustryManagement.Models;

namespace CarManufacturingIndustryManagement.Services
{
    public interface IFeedbackService
    {
        IEnumerable<Feedback> GetAll();
        IEnumerable<Feedback> GetByQuery(string query);
        void Delete(int id);
        Feedback Create(Feedback feedback);
    }
}

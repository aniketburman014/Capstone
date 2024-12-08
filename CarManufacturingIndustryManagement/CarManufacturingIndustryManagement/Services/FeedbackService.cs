using CarManufacturingIndustryManagement.Models;
using CarManufacturingIndustryManagement.Repo;
namespace CarManufacturingIndustryManagement.Services
{
    public class FeedbackService : IFeedbackService
    {
        
        private readonly IFeedbackRepo _repository;

        public FeedbackService(IFeedbackRepo repository)
        {
            _repository = repository;
        }

        public IEnumerable<Feedback> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Feedback> GetByQuery(string query)
        {

            return _repository.GetByQuery(f =>
                f.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                f.Email.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                f.Message.Contains(query, StringComparison.OrdinalIgnoreCase));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
        public Feedback Create(Feedback feedback)
        {
            feedback.CreatedAt = DateTime.UtcNow; // Automatically set the created date
            return _repository.Create(feedback);
        }
    }
}

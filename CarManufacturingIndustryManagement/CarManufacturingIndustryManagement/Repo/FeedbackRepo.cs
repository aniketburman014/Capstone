using CarManufacturingIndustryManagement.Data;
using CarManufacturingIndustryManagement.Models;

namespace CarManufacturingIndustryManagement.Repo
{
    public class FeedbackRepo:IFeedbackRepo
    {
        private readonly AppDbContext _context;

        public FeedbackRepo(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Feedback> GetAll()
        {
            return _context.Feedbacks.ToList(); // Fetch all records from the database
        }

        public IEnumerable<Feedback> GetByQuery(Func<Feedback, bool> predicate)
        {
            return _context.Feedbacks.AsEnumerable().Where(predicate);
            // `AsEnumerable` is used to evaluate the predicate in memory.
        }


        public void Delete(int id)
        {
            var feedback = _context.Feedbacks.FirstOrDefault(f => f.Id == id); // Find the feedback
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback); // Mark the entity for deletion
                _context.SaveChanges(); // Persist changes to the database
            }
        }

        public Feedback Create(Feedback feedback)
        {

            feedback.CreatedAt = DateTime.UtcNow; // Set created timestamp
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges(); // Save to database
            return feedback;
        }
    }
}

using CarManufacturingIndustryManagement.Models;

namespace CarManufacturingIndustryManagement.Repo
{
    public interface IFeedbackRepo
    {
        IEnumerable<Feedback> GetAll();
        IEnumerable<Feedback> GetByQuery(Func<Feedback, bool> predicate);
        void Delete(int id);
        Feedback Create(Feedback feedback);
    }
}

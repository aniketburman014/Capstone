using CarManufacturingIndustryManagement.Models;
using System.Threading.Tasks;
using CarManufacturingIndustryManagement.DTO;
namespace CarManufacturingIndustryManagement.Services
{
    public interface IUserService
    {
        Task<ServiceResponse> RegisterUser(User user);
        Task<ServiceResponse> AuthenticateUser(LoginRequest loginRequest);
    }

    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}

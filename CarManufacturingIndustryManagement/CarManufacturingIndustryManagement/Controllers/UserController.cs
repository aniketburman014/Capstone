using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarManufacturingIndustryManagement.Models;
using CarManufacturingIndustryManagement.Services;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CarManufacturingIndustryManagement.DTO;

namespace CarManufacturingIndustryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Signup Endpoint
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] User user)
        {
            var result = await _userService.RegisterUser(user);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(new { message = result.Message, token = result.Token });
        }

        // Login Endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _userService.AuthenticateUser(loginRequest);
            if (!result.Success)
            {
                return Unauthorized(new { message = result.Message });
            }

            return Ok(new { token = result.Token });
        }
    }

    
}

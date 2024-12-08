using CarManufacturingIndustryManagement.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CarManufacturingIndustryManagement.Models;
using CarManufacturingIndustryManagement.DTO;
namespace CarManufacturingIndustryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext context;
        public AuthController(AppDbContext context)
        {
            this.context = context;
        }
        // get all user
        [HttpGet("all-users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            // Retrieve all users from the database
            var users = await context.Users
                .Select(u => new
                {
                    u.UserId,
                    u.Username,
                    u.Email,
                    u.Role,
                    
                })
                .ToListAsync();

            return Ok(users);
        }
        [HttpGet("search-user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchUser(string query)
        {
            // Determine if the query is an integer (for ID)
            bool isId = int.TryParse(query, out int userId);

            var users = await context.Users
                .Where(u =>
                    u.Username.ToLower().Contains(query.ToLower()) ||  // Case-insensitive username match
                    u.Email.ToLower().Contains(query.ToLower()) ||     // Case-insensitive email match
                    (isId && u.UserId == userId) ||                     // Match by ID if query is a number
                    u.Role.ToLower().Equals(query.ToLower()))           // Case-insensitive role match
                .Select(u => new
                {
                    u.UserId,
                    u.Username,
                    u.Email,
                    u.Role
                })
                .ToListAsync();

            return Ok(users);
        }
        [HttpPut("update-user/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody]UpdateUser updatedUser)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }
            Console.WriteLine(updatedUser);
            user.Username = updatedUser.Username ?? user.Username;
            user.Email = updatedUser.Email ?? user.Email;
            user.Role = updatedUser.Role ?? user.Role;
            user.UpdatedAt=DateTime.Now;
            await context.SaveChangesAsync();

            return Ok(new { Message = "User updated successfully" });
        }

        [HttpDelete("delete-user/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return Ok(new { Message = "User deleted successfully" });
        }




    }
}

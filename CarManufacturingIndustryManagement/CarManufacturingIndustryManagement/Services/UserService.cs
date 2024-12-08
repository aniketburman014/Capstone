using CarManufacturingIndustryManagement.Data;
using CarManufacturingIndustryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using CarManufacturingIndustryManagement.DTO;

namespace CarManufacturingIndustryManagement.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly JwtServices _jwtServices;

        public UserService(AppDbContext context, JwtServices jwtServices)
        {
            _context = context;
            _jwtServices = jwtServices;
        }

        public async Task<ServiceResponse> RegisterUser(User user)
        {
            // Check if user already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User already exists with this email."
                };
            }

            // Hash the password using BCrypt
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            user.IsActive = true;

            // Add user to database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generate JWT token
            var token = _jwtServices.GenerateToken(user);

            return new ServiceResponse
            {
                Success = true,
                Message = "Signup successful. You are now logged in.",
                Token = token,
                
            };
        }

        public async Task<ServiceResponse> AuthenticateUser(LoginRequest loginRequest)
        {
            // Find user by email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);
            if (user == null || !user.IsActive)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Invalid credentials or user is inactive."
                };
            }

            // Verify password
            if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Invalid credentials."
                };
            }

            // Generate JWT token
            var token = _jwtServices.GenerateToken(user);
            return new ServiceResponse
            {
                Success = true,
                Token = token
            };
        }
    }
}

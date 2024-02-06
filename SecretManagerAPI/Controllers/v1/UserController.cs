using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SecretManagerAPI.Models;
using SecretManagerModels.Models;
using Serilog;

namespace SecretManagerAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/users")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly SecretManagerDBContext _context;

        public UserController(SecretManagerDBContext context, IAPISettings config)
        {
            _context = context;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                Log.Information("Number of users retrieved {NumberOfUsers}", users.Count);
                return Ok(users);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving users");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    Log.Warning("User Not found with ID {userID}", id);
                    return NotFound();
                }

                Log.Information("User found with ID {userID}, {userInfo}", id, user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error retrieving user with ID {id}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                Log.Information("user {username} created successfully", user.Name);
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error creating user {userInfo}", user);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            try
            {
                var existingUser = await _context.Users.FindAsync(id);

                if (existingUser == null)
                {
                    Log.Warning("User Not found with ID {userID}", id);
                    return NotFound();
                }

                existingUser.Name = user.Name;
                // Update other properties as needed

                await _context.SaveChangesAsync();

                Log.Information("User updated succesfully {userID}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error updating user with ID {id}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    Log.Warning("User Not found for deletion with ID {userID}", id);
                    return NotFound();
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error deleting user with ID {id}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
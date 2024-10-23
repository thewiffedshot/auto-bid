using Microsoft.AspNetCore.Mvc;
using WebApi.DataContext.Models; 
using AutoBid.WebApi.DataContext;
using WebApi.Interfaces.Models;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AutoBidDbContext _context;

        public UserController(AutoBidDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user == null)
            {
                return BadRequest("User is required.");
            }

            var existingUser = await _context.Users.AnyAsync(
                e => e.Username == user.Username || 
                e.Email == user.Email
            );

            if (existingUser)
            {
                return BadRequest("User already exists.");
            }

            var userEntity = user.ToEntity();
            _context.Users.Add(userEntity);

            await _context.SaveChangesAsync();

            return Created("User created.", userEntity.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user == null)
            {
                return BadRequest("User is required.");
            }

            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            _context.Users.Update(user.ToEntity(id));
            await _context.SaveChangesAsync();

            return Ok("User updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("User deleted.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user.ToModel());
        }

        [HttpGet("Exists")]
        public async Task<IActionResult> UserExists([FromBody] UserModel user)
        {
            if (user == null)
            {
                return BadRequest("User is required.");
            }

            var existingUser = await _context.Users.AnyAsync(
                e => e.Username == user.Username || 
                e.Email == user.Email
            );

            if (!existingUser)
            {
                return NotFound("User not found.");
            }

            return Ok(existingUser);
        }
    }
}
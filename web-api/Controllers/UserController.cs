using Microsoft.AspNetCore.Mvc;
using WebApi.Data.Models; 
using AutoBid.WebApi.Data;
using WebApi.Interfaces.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces.Mappers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AutoBidDbContext _context;
        private readonly IMapper<UserModel, User> _userMapper;

        public UserController(AutoBidDbContext context, IMapper<UserModel, User> userMapper)
        {
            _context = context;
            _userMapper = userMapper;
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

            var userEntity = _userMapper.Map(user);
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

            _userMapper.Map(user, existingUser);
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

            return Ok("User exists.");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MentiaApi.Data;
using MentiaApi.Models;

namespace MentiaApi.Controllers
{
    /// <summary>
    /// API controller for managing users. Supports both v1 and v2 via URL segment versioning.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class UsersController : ControllerBase
    {
        private readonly MentiaDbContext _context;

        public UsersController(MentiaDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A list of users.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }

        /// <summary>
        /// Gets a user by its identifier.
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>The user if found, otherwise 404.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">User to create</param>
        /// <returns>Created user with 201 status.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";
            return CreatedAtAction(nameof(GetUser), new { id = user.Id, version }, user);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <param name="user">User with updated values</param>
        /// <returns>No content if successful, 404 if not found.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>No content if deleted, 404 if not found.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
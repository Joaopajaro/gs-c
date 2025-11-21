using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MentiaApi.Data;
using MentiaApi.Models;

namespace MentiaApi.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class MentoriasController : ControllerBase
    {
        private readonly MentiaDbContext _context;

        public MentoriasController(MentiaDbContext context)
        {
            _context = context;
        }

   
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Mentoria>>> GetMentorias()
        {
            return await _context.Mentorias.Include(m => m.Mentor).ToListAsync();
        }

    
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Mentoria), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Mentoria>> GetMentoria(int id)
        {
            var mentoria = await _context.Mentorias.Include(m => m.Mentor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mentoria == null)
            {
                return NotFound();
            }
            return mentoria;
        }

   
        [HttpPost]
        [ProducesResponseType(typeof(Mentoria), StatusCodes.Status201Created)]
        public async Task<ActionResult<Mentoria>> PostMentoria(Mentoria mentoria)
        {
            _context.Mentorias.Add(mentoria);
            await _context.SaveChangesAsync();
            var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";
            return CreatedAtAction(nameof(GetMentoria), new { id = mentoria.Id, version }, mentoria);
        }

   
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutMentoria(int id, Mentoria mentoria)
        {
            if (id != mentoria.Id)
            {
                return BadRequest();
            }
            _context.Entry(mentoria).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Mentorias.Any(e => e.Id == id))
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

      
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMentoria(int id)
        {
            var mentoria = await _context.Mentorias.FindAsync(id);
            if (mentoria == null)
            {
                return NotFound();
            }
            _context.Mentorias.Remove(mentoria);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

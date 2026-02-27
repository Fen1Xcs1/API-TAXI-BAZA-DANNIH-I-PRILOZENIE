using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxiAPI.Data;
using TaxiAPI.Models;

namespace TaxiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class УслугиController : ControllerBase
    {
        private readonly TaxiDbContext _context;

        public УслугиController(TaxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Услуги
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Услуги>>> GetУслуги()
        {
            return await _context.Услуги.Where(u => u.активна).ToListAsync();
        }

        // GET: api/Услуги/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Услуги>> GetУслуга(int id)
        {
            var услуга = await _context.Услуги.FindAsync(id);

            if (услуга == null)
            {
                return NotFound();
            }

            return услуга;
        }

        // POST: api/Услуги
        [HttpPost]
        public async Task<ActionResult<Услуги>> PostУслуга(Услуги услуга)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Услуги.Add(услуга);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetУслуга), new { id = услуга.услуга_id }, услуга);
        }

        // PUT: api/Услуги/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutУслуга(int id, Услуги услуга)
        {
            if (id != услуга.услуга_id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(услуга).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!УслугаExists(id))
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

        // DELETE: api/Услуги/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteУслуга(int id)
        {
            var услуга = await _context.Услуги.FindAsync(id);
            if (услуга == null)
            {
                return NotFound();
            }

            // Проверка, используется ли услуга в заказах
            var используется = await _context.УслугиВЗаказе.AnyAsync(uz => uz.услуга_id == id);
            if (используется)
            {
                // Мягкое удаление - деактивируем
                услуга.активна = false;
                await _context.SaveChangesAsync();
                return NoContent();
            }

            _context.Услуги.Remove(услуга);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool УслугаExists(int id)
        {
            return _context.Услуги.Any(e => e.услуга_id == id);
        }
    }
}
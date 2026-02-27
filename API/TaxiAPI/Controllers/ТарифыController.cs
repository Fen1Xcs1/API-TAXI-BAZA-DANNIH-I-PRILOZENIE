using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxiAPI.Data;
using TaxiAPI.DTO;
using TaxiAPI.Models;

namespace TaxiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ТарифыController : ControllerBase
    {
        private readonly TaxiDbContext _context;

        public ТарифыController(TaxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Тарифы
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ТарифDto>>> GetТарифы()
        {
            var тарифы = await _context.Тарифы
                .Select(t => new ТарифDto
                {
                    тариф_id = t.тариф_id,
                    название = t.название,
                    цена = t.цена,
                    описание = t.описание
                })
                .ToListAsync();

            return Ok(тарифы);
        }

        // GET: api/Тарифы/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ТарифDto>> GetТариф(int id)
        {
            var тариф = await _context.Тарифы
                .Where(t => t.тариф_id == id)
                .Select(t => new ТарифDto
                {
                    тариф_id = t.тариф_id,
                    название = t.название,
                    цена = t.цена,
                    описание = t.описание
                })
                .FirstOrDefaultAsync();

            if (тариф == null)
            {
                return NotFound();
            }

            return Ok(тариф);
        }

        // POST: api/Тарифы
        [HttpPost]
        public async Task<ActionResult<ТарифDto>> PostТариф(Тарифы тариф)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Тарифы.Add(тариф);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetТариф), new { id = тариф.тариф_id }, тариф);
        }

        // PUT: api/Тарифы/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutТариф(int id, Тарифы тариф)
        {
            if (id != тариф.тариф_id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(тариф).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ТарифExists(id))
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

        // DELETE: api/Тарифы/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteТариф(int id)
        {
            var тариф = await _context.Тарифы.FindAsync(id);
            if (тариф == null)
            {
                return NotFound();
            }

            // Проверка, используется ли тариф в автомобилях
            var используется = await _context.Автомобили.AnyAsync(a => a.тариф_id == id);
            if (используется)
            {
                return BadRequest("Невозможно удалить тариф, так как он используется в автомобилях");
            }

            _context.Тарифы.Remove(тариф);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ТарифExists(int id)
        {
            return _context.Тарифы.Any(e => e.тариф_id == id);
        }
    }
}
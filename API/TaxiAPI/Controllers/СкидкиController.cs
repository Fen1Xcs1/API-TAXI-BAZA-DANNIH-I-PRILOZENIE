using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxiAPI.Data;
using TaxiAPI.Models;

namespace TaxiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class СкидкиController : ControllerBase
    {
        private readonly TaxiDbContext _context;

        public СкидкиController(TaxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Скидки
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Скидки>>> GetСкидки()
        {
            var текущаяДата = DateTime.Now;
            return await _context.Скидки
                .Where(s => s.активна && s.дата_начала <= текущаяДата && s.дата_окончания >= текущаяДата)
                .ToListAsync();
        }

        // GET: api/Скидки/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Скидки>> GetСкидка(int id)
        {
            var скидка = await _context.Скидки.FindAsync(id);

            if (скидка == null)
            {
                return NotFound();
            }

            return скидка;
        }

        // GET: api/Скидки/пользователя/5
        [HttpGet("пользователя/{пользовательId}")]
        public async Task<ActionResult<IEnumerable<Скидки>>> GetСкидкиПользователя(int пользовательId)
        {
            var текущаяДата = DateTime.Now;

            // Проверка существования пользователя
            var пользователь = await _context.Пользователи.FindAsync(пользовательId);
            if (пользователь == null)
            {
                return NotFound($"Пользователь с ID {пользовательId} не найден");
            }

            var скидки = await _context.Скидки
                .Where(s => s.активна && s.дата_начала <= текущаяДата && s.дата_окончания >= текущаяДата)
                .ToListAsync();

            return Ok(скидки);
        }

        // POST: api/Скидки
        [HttpPost]
        public async Task<ActionResult<Скидки>> PostСкидка(Скидки скидка)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Скидки.Add(скидка);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetСкидка), new { id = скидка.скидка_id }, скидка);
        }

        // PUT: api/Скидки/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutСкидка(int id, Скидки скидка)
        {
            if (id != скидка.скидка_id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(скидка).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!СкидкаExists(id))
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

        private bool СкидкаExists(int id)
        {
            return _context.Скидки.Any(e => e.скидка_id == id);
        }
    }
}
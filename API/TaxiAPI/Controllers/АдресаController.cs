using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxiAPI.Data;
using TaxiAPI.DTO;
using TaxiAPI.Models;

namespace TaxiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class АдресаController : ControllerBase
    {
        private readonly TaxiDbContext _context;

        public АдресаController(TaxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Адреса
        [HttpGet]
        public async Task<ActionResult<IEnumerable<АдресDto>>> GetАдреса()
        {
            var адреса = await _context.Адреса
                .Select(a => new АдресDto
                {
                    адрес_id = a.адрес_id,
                    город = a.город,
                    улица = a.улица,
                    дом = a.дом,
                    подъезд = a.подъезд,
                    квартира = a.квартира,
                    начальный_адрес = a.начальный_адрес,
                    конечный_адрес = a.конечный_адрес
                })
                .ToListAsync();

            return Ok(адреса);
        }

        // GET: api/Адреса/5
        [HttpGet("{id}")]
        public async Task<ActionResult<АдресDto>> GetАдрес(int id)
        {
            var адрес = await _context.Адреса
                .Where(a => a.адрес_id == id)
                .Select(a => new АдресDto
                {
                    адрес_id = a.адрес_id,
                    город = a.город,
                    улица = a.улица,
                    дом = a.дом,
                    подъезд = a.подъезд,
                    квартира = a.квартира,
                    начальный_адрес = a.начальный_адрес,
                    конечный_адрес = a.конечный_адрес
                })
                .FirstOrDefaultAsync();

            if (адрес == null)
            {
                return NotFound();
            }

            return Ok(адрес);
        }

        // POST: api/Адреса
        [HttpPost]
        public async Task<ActionResult<Адреса>> PostАдрес(Адреса адрес)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Адреса.Add(адрес);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetАдрес), new { id = адрес.адрес_id }, адрес);
        }

        // PUT: api/Адреса/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutАдрес(int id, Адреса адрес)
        {
            if (id != адрес.адрес_id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(адрес).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!АдресExists(id))
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

        private bool АдресExists(int id)
        {
            return _context.Адреса.Any(e => e.адрес_id == id);
        }
    }
}
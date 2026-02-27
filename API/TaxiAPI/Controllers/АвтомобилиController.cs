using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxiAPI.Data;
using TaxiAPI.DTO;
using TaxiAPI.Models;

namespace TaxiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class АвтомобилиController : ControllerBase
    {
        private readonly TaxiDbContext _context;

        public АвтомобилиController(TaxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Автомобили
        [HttpGet]
        public async Task<ActionResult<IEnumerable<АвтомобильDto>>> GetАвтомобили()
        {
            var автомобили = await _context.Автомобили
                .Include(a => a.Тариф)
                .Select(a => new АвтомобильDto
                {
                    автомобиль_id = a.автомобиль_id,
                    модель = a.модель,
                    марка = a.марка,
                    номерной_знак = a.номерной_знак,
                    цвет = a.цвет,
                    тариф_id = a.тариф_id,
                    класс_авто = a.класс_авто,
                    активен = a.активен,
                    Тариф = a.Тариф != null ? new ТарифDto
                    {
                        тариф_id = a.Тариф.тариф_id,
                        название = a.Тариф.название,
                        цена = a.Тариф.цена,
                        описание = a.Тариф.описание
                    } : null
                })
                .ToListAsync();

            return Ok(автомобили);
        }

        // GET: api/Автомобили/активные
        [HttpGet("активные")]
        public async Task<ActionResult<IEnumerable<АвтомобильDto>>> GetАктивныеАвтомобили()
        {
            var автомобили = await _context.Автомобили
                .Where(a => a.активен == true)
                .Include(a => a.Тариф)
                .Select(a => new АвтомобильDto
                {
                    автомобиль_id = a.автомобиль_id,
                    модель = a.модель,
                    марка = a.марка,
                    номерной_знак = a.номерной_знак,
                    цвет = a.цвет,
                    тариф_id = a.тариф_id,
                    класс_авто = a.класс_авто,
                    активен = a.активен,
                    Тариф = a.Тариф != null ? new ТарифDto
                    {
                        тариф_id = a.Тариф.тариф_id,
                        название = a.Тариф.название,
                        цена = a.Тариф.цена,
                        описание = a.Тариф.описание
                    } : null
                })
                .ToListAsync();

            return Ok(автомобили);
        }

        // GET: api/Автомобили/5
        [HttpGet("{id}")]
        public async Task<ActionResult<АвтомобильDto>> GetАвтомобиль(int id)
        {
            var автомобиль = await _context.Автомобили
                .Include(a => a.Тариф)
                .Where(a => a.автомобиль_id == id)
                .Select(a => new АвтомобильDto
                {
                    автомобиль_id = a.автомобиль_id,
                    модель = a.модель,
                    марка = a.марка,
                    номерной_знак = a.номерной_знак,
                    цвет = a.цвет,
                    тариф_id = a.тариф_id,
                    класс_авто = a.класс_авто,
                    активен = a.активен,
                    Тариф = a.Тариф != null ? new ТарифDto
                    {
                        тариф_id = a.Тариф.тариф_id,
                        название = a.Тариф.название,
                        цена = a.Тариф.цена,
                        описание = a.Тариф.описание
                    } : null
                })
                .FirstOrDefaultAsync();

            if (автомобиль == null)
            {
                return NotFound();
            }

            return Ok(автомобиль);
        }

        // GET: api/Автомобили/потарифу/5
        [HttpGet("потарифу/{тарифId}")]
        public async Task<ActionResult<IEnumerable<АвтомобильDto>>> GetАвтомобилиПоТарифу(int тарифId)
        {
            var автомобили = await _context.Автомобили
                .Where(a => a.тариф_id == тарифId && a.активен == true)
                .Include(a => a.Тариф)
                .Select(a => new АвтомобильDto
                {
                    автомобиль_id = a.автомобиль_id,
                    модель = a.модель,
                    марка = a.марка,
                    номерной_знак = a.номерной_знак,
                    цвет = a.цвет,
                    тариф_id = a.тариф_id,
                    класс_авто = a.класс_авто,
                    активен = a.активен,
                    Тариф = a.Тариф != null ? new ТарифDto
                    {
                        тариф_id = a.Тариф.тариф_id,
                        название = a.Тариф.название,
                        цена = a.Тариф.цена,
                        описание = a.Тариф.описание
                    } : null
                })
                .ToListAsync();

            return Ok(автомобили);
        }

        // POST: api/Автомобили
        [HttpPost]
        public async Task<ActionResult<Автомобили>> PostАвтомобиль(Автомобили автомобиль)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Автомобили.Add(автомобиль);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetАвтомобиль), new { id = автомобиль.автомобиль_id }, автомобиль);
        }

        // PUT: api/Автомобили/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutАвтомобиль(int id, Автомобили автомобиль)
        {
            if (id != автомобиль.автомобиль_id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(автомобиль).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!АвтомобильExists(id))
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

        // PATCH: api/Автомобили/5/деактивировать
        [HttpPatch("{id}/деактивировать")]
        public async Task<IActionResult> ДеактивироватьАвтомобиль(int id)
        {
            var автомобиль = await _context.Автомобили.FindAsync(id);
            if (автомобиль == null)
            {
                return NotFound();
            }

            автомобиль.активен = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Автомобили/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteАвтомобиль(int id)
        {
            var автомобиль = await _context.Автомобили.FindAsync(id);
            if (автомобиль == null)
            {
                return NotFound();
            }

            _context.Автомобили.Remove(автомобиль);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool АвтомобильExists(int id)
        {
            return _context.Автомобили.Any(e => e.автомобиль_id == id);
        }
    }
}
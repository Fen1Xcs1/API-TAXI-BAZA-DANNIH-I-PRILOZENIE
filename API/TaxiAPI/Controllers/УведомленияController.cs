using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxiAPI.Data;
using TaxiAPI.Models;

namespace TaxiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class УведомленияController : ControllerBase
    {
        private readonly TaxiDbContext _context;

        public УведомленияController(TaxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Уведомления/пользователя/5
        [HttpGet("пользователя/{пользовательId}")]
        public async Task<ActionResult<IEnumerable<Уведомления>>> GetУведомленияПользователя(int пользовательId)
        {
            var пользователь = await _context.Пользователи.FindAsync(пользовательId);
            if (пользователь == null)
            {
                return NotFound($"Пользователь с ID {пользовательId} не найден");
            }

            return await _context.Уведомления
                .Where(u => u.пользователи_id == пользовательId)
                .OrderByDescending(u => u.временная_метка)
                .ToListAsync();
        }

        // GET: api/Уведомления/пользователя/5/непрочитанные
        [HttpGet("пользователя/{пользовательId}/непрочитанные")]
        public async Task<ActionResult<IEnumerable<Уведомления>>> GetНепрочитанныеУведомления(int пользовательId)
        {
            var пользователь = await _context.Пользователи.FindAsync(пользовательId);
            if (пользователь == null)
            {
                return NotFound($"Пользователь с ID {пользовательId} не найден");
            }

            return await _context.Уведомления
                .Where(u => u.пользователи_id == пользовательId && u.прочитано == false)
                .OrderByDescending(u => u.временная_метка)
                .ToListAsync();
        }

        // GET: api/Уведомления/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Уведомления>> GetУведомление(int id)
        {
            var уведомление = await _context.Уведомления.FindAsync(id);

            if (уведомление == null)
            {
                return NotFound();
            }

            return уведомление;
        }

        // PATCH: api/Уведомления/5/прочитать
        [HttpPatch("{id}/прочитать")]
        public async Task<IActionResult> ОтметитьКакПрочитанное(int id)
        {
            var уведомление = await _context.Уведомления.FindAsync(id);
            if (уведомление == null)
            {
                return NotFound();
            }

            уведомление.прочитано = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/Уведомления/пользователя/5/прочитатьвсе
        [HttpPatch("пользователя/{пользовательId}/прочитатьвсе")]
        public async Task<IActionResult> ОтметитьВсеКакПрочитанные(int пользовательId)
        {
            var пользователь = await _context.Пользователи.FindAsync(пользовательId);
            if (пользователь == null)
            {
                return NotFound($"Пользователь с ID {пользовательId} не найден");
            }

            var уведомления = await _context.Уведомления
                .Where(u => u.пользователи_id == пользовательId && u.прочитано == false)
                .ToListAsync();

            foreach (var уведомление in уведомления)
            {
                уведомление.прочитано = true;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Уведомления
        [HttpPost]
        public async Task<ActionResult<Уведомления>> PostУведомление(Уведомления уведомление)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Проверка существования пользователя
            var пользователь = await _context.Пользователи.FindAsync(уведомление.пользователи_id);
            if (пользователь == null)
            {
                return BadRequest($"Пользователь с ID {уведомление.пользователи_id} не найден");
            }

            // Проверка существования заказа (если указан)
            if (уведомление.заказ_id.HasValue)
            {
                var заказ = await _context.Заказы.FindAsync(уведомление.заказ_id.Value);
                if (заказ == null)
                {
                    return BadRequest($"Заказ с ID {уведомление.заказ_id} не найден");
                }
            }

            уведомление.временная_метка = DateTime.Now;
            уведомление.прочитано = false;

            _context.Уведомления.Add(уведомление);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetУведомление), new { id = уведомление.уведомление_id }, уведомление);
        }

        // DELETE: api/Уведомления/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteУведомление(int id)
        {
            var уведомление = await _context.Уведомления.FindAsync(id);
            if (уведомление == null)
            {
                return NotFound();
            }

            _context.Уведомления.Remove(уведомление);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxiAPI.Data;
using TaxiAPI.DTO;
using TaxiAPI.Models;

namespace TaxiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ПользователиController : ControllerBase
    {
        private readonly TaxiDbContext _context;

        public ПользователиController(TaxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Пользователи
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ПользовательDto>>> GetПользователи()
        {
            var пользователи = await _context.Пользователи
                .Select(p => new ПользовательDto
                {
                    пользователи_id = p.пользователи_id,
                    ФИО = p.ФИО,
                    номер_телефона = p.номер_телефона,
                    статус = p.статус,
                    логин = p.логин,
                    дата_регистрации = p.дата_регистрации
                })
                .ToListAsync();

            return Ok(пользователи);
        }

        // GET: api/Пользователи/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ПользовательDto>> GetПользователь(int id)
        {
            var пользователь = await _context.Пользователи
                .Where(p => p.пользователи_id == id)
                .Select(p => new ПользовательDto
                {
                    пользователи_id = p.пользователи_id,
                    ФИО = p.ФИО,
                    номер_телефона = p.номер_телефона,
                    статус = p.статус,
                    логин = p.логин,
                    дата_регистрации = p.дата_регистрации
                })
                .FirstOrDefaultAsync();

            if (пользователь == null)
            {
                return NotFound();
            }

            return Ok(пользователь);
        }

        // GET: api/Пользователи/потелефону/79161234567
        [HttpGet("потелефону/{телефон}")]
        public async Task<ActionResult<ПользовательDto>> GetПользовательПоТелефону(string телефон)
        {
            var пользователь = await _context.Пользователи
                .Where(p => p.номер_телефона == телефон)
                .Select(p => new ПользовательDto
                {
                    пользователи_id = p.пользователи_id,
                    ФИО = p.ФИО,
                    номер_телефона = p.номер_телефона,
                    статус = p.статус,
                    логин = p.логин,
                    дата_регистрации = p.дата_регистрации
                })
                .FirstOrDefaultAsync();

            if (пользователь == null)
            {
                return NotFound();
            }

            return Ok(пользователь);
        }

        // POST: api/Пользователи/авторизация
        [HttpPost("авторизация")]
        public async Task<ActionResult<ПользовательDto>> Авторизация(АвторизацияRequest request)
        {
            var пользователь = await _context.Пользователи
                .Where(p => p.логин == request.логин && p.пароль == request.пароль)
                .Select(p => new ПользовательDto
                {
                    пользователи_id = p.пользователи_id,
                    ФИО = p.ФИО,
                    номер_телефона = p.номер_телефона,
                    статус = p.статус,
                    логин = p.логин,
                    дата_регистрации = p.дата_регистрации
                })
                .FirstOrDefaultAsync();

            if (пользователь == null)
            {
                return Unauthorized(new { message = "Неверный логин или пароль" });
            }

            return Ok(пользователь);
        }

        // POST: api/Пользователи
        [HttpPost]
        public async Task<ActionResult<Пользователи>> PostПользователь(Пользователи пользователь)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Проверка уникальности логина и телефона
            if (await _context.Пользователи.AnyAsync(p => p.логин == пользователь.логин))
            {
                return BadRequest("Пользователь с таким логином уже существует");
            }

            if (await _context.Пользователи.AnyAsync(p => p.номер_телефона == пользователь.номер_телефона))
            {
                return BadRequest("Пользователь с таким номером телефона уже существует");
            }

            пользователь.дата_регистрации = DateTime.Now;
            пользователь.статус = "активен";

            _context.Пользователи.Add(пользователь);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetПользователь), new { id = пользователь.пользователи_id }, пользователь);
        }

        // PUT: api/Пользователи/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutПользователь(int id, Пользователи пользователь)
        {
            if (id != пользователь.пользователи_id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(пользователь).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ПользовательExists(id))
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

        private bool ПользовательExists(int id)
        {
            return _context.Пользователи.Any(e => e.пользователи_id == id);
        }
    }

    public class АвторизацияRequest
    {
        public string логин { get; set; }
        public string пароль { get; set; }
    }
}
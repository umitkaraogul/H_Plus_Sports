using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using H_Plus_Sports.Models;

namespace HPlusSportsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Salespersons")]
    public class SalespersonsController : Controller
    {
        private readonly H_Plus_SportsContext _context;

        public SalespersonsController(H_Plus_SportsContext context)
        {
            _context = context;
        }

        private bool SalespersonExists(int id)
        {
            return _context.Salesperson.Any(e => e.SalespersonId == id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Salesperson>))]
        public IActionResult GetSalesperson()
        {
            return new ObjectResult(_context.Salesperson);
        }

        [HttpGet("{id}")]
        [Produces(typeof(Salesperson))]
        public async Task<IActionResult> GetSalesperson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salesperson = await _context.Salesperson.SingleOrDefaultAsync(m => m.SalespersonId == id);

            if (salesperson == null)
            {
                return NotFound();
            }

            return Ok(salesperson);
        }

        [HttpPut("{id}")]
        [Produces(typeof(Salesperson))]
        public async Task<IActionResult> PutSalesperson([FromRoute] int id, [FromBody] Salesperson salesperson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesperson.SalespersonId)
            {
                return BadRequest();
            }

            _context.Entry(salesperson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(salesperson);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalespersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpPost]
        [Produces(typeof(Salesperson))]
        public async Task<IActionResult> PostSalesperson([FromBody] Salesperson salesperson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Salesperson.Add(salesperson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalesperson", new { id = salesperson.SalespersonId }, salesperson);
        }

        [HttpDelete("{id}")]
        [Produces(typeof(Salesperson))]
        public async Task<IActionResult> DeleteSalesperson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salesperson = await _context.Salesperson.SingleOrDefaultAsync(m => m.SalespersonId == id);
            if (salesperson == null)
            {
                return NotFound();
            }

            _context.Salesperson.Remove(salesperson);
            await _context.SaveChangesAsync();

            return Ok(salesperson);
        }
    }
}
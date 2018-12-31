using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using H_Plus_Sports.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public IActionResult GetSalesperson()
        {
            return new ObjectResult(_context.Salesperson);
        }

        [HttpGet("{id}",Name = "GetSalesperson")]
        public async Task<IActionResult> GetSalesperson([FromRoute] int id)
        {
            var salesPerson = await _context.Salesperson.SingleOrDefaultAsync(m => m.SalespersonId == id);
            
            return Ok(salesPerson);
        }

        [HttpPost]
        public async Task<IActionResult> PostSalesperson([FromBody] Salesperson salesperson)
        {
            _context.Salesperson.Add(salesperson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("getSalesperson", new {id = salesperson.SalespersonId},salesperson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesperson([FromRoute] int id, [FromBody] Salesperson salesperson)
        {
            _context.Entry(salesperson).State = EntityState.Modified;

            await _context.SaveChangesAsync();
           
            return Ok(salesperson);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesperson([FromRoute] int id)
        {
            var salesperson = await _context.Salesperson.SingleOrDefaultAsync(m => m.SalespersonId == id);

            _context.Salesperson.Remove(salesperson);
            
            await _context.SaveChangesAsync();
            
            return Ok(salesperson);
        }
    }
}
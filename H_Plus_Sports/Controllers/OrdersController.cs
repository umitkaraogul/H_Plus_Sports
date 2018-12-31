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
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        private readonly H_Plus_SportsContext _context;
        public OrdersController(H_Plus_SportsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetOrder()
        {
            return new ObjectResult(_context.Order);
        }

        [HttpGet("{id}",Name = "GetOrder")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(m => m.OrderId == id);
            
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("getOrder", new {id = order.OrderId},order);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromRoute] int id, [FromBody] Order order)
        {
            _context.Entry(order).State = EntityState.Modified;

            await _context.SaveChangesAsync();
           
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(m => m.OrderId == id);

            _context.Order.Remove(order);
            
            await _context.SaveChangesAsync();
            
            return Ok(order);
        }
    }
}
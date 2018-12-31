using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using H_Plus_Sports.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace H_Plus_Sports.Controllers
{
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : Controller
    {
        private readonly H_Plus_SportsContext _context;

        public CustomersController(H_Plus_SportsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCustomer()
        {
            return new ObjectResult(_context.Customer);
        }

        [HttpGet("{id}",Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.CustomerId == id);
            
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("getCustomer", new {id = customer.CustomerId},customer);
          
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;

            await _context.SaveChangesAsync();
           
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.CustomerId == id);

            _context.Customer.Remove(customer);
            
            await _context.SaveChangesAsync();
            
            return Ok(customer);
        }
    }
}
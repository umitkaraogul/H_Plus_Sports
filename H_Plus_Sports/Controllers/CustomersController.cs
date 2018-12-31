using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using H_Plus_Sports.Models;
using System.Net;

namespace HPlusSportsAPI.Controllers
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

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerId == id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Customer>))]
        public IActionResult GetCustomer()
        {
            var results = new ObjectResult(_context.Customer)
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            Request.HttpContext.Response.Headers.Add("X-Total-Count", _context.Customer.Count().ToString());

            return results;
        }

        [HttpGet("{id}")]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPut("{id}")]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpPost]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        [HttpDelete("{id}")]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }
    }
}
using System.Threading.Tasks;
using H_Plus_Sports.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using H_Plus_Sports.Models;

namespace H_Plus_Sports.Controllers
{
    [Produces("application/json")]
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orders;

        public OrdersController(IOrderRepository orders)
        {
            _orders = orders;
        }

        private async Task<bool> OrderExists(int id)
        {
            return await _orders.Exists(id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Order>))]
        public IActionResult GetOrder()
        {
            return new ObjectResult(_orders.GetAll());
        }

        [HttpGet("{id}")]
        [Produces(typeof(Order))]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPut("{id}")]
        [Produces(typeof(Order))]
        public async Task<IActionResult> PutOrder([FromRoute] int id, [FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.OrderId)
            {
                return BadRequest();
            }

            try
            {
                await _orders.Update(order);
                return Ok(order);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpPost]
        [Produces(typeof(Order))]
        public async Task<IActionResult> PostOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orders.Add(order);
            }
            catch (DbUpdateException)
            {
                if (!await OrderExists(order.OrderId))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        [HttpDelete("{id}")]
        [Produces(typeof(Order))]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await OrderExists(id))
            {
                return NotFound();
            }

            await _orders.Remove(id);

            return Ok();
        }
    }
}
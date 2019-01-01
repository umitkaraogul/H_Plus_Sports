using System.Threading.Tasks;
using H_Plus_Sports.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using H_Plus_Sports.Models;

namespace H_Plus_Sports.Controllers
{
    [Produces("application/json")]
    [Route("api/OrderItems")]
    public class OrderItemsController : Controller
    {
        private readonly IOrderItemRepository _orderItems;

        public OrderItemsController(IOrderItemRepository orderItems)
        {
            _orderItems = orderItems;
        }

        private async Task<bool> OrderItemExists(int id)
        {
            return await _orderItems.Exists(id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<OrderItem>))]
        public IActionResult GetOrderItem()
        {
            return new ObjectResult(_orderItems.GetAll());
        }

        [HttpGet("{id}")]
        [Produces(typeof(OrderItem))]
        public async Task<IActionResult> GetOrderItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderItem = await _orderItems.Find(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }

        [HttpPut("{id}")]
        [Produces(typeof(OrderItem))]
        public async Task<IActionResult> PutOrderItem([FromRoute] int id, [FromBody] OrderItem orderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderItem.OrderItemId)
            {
                return BadRequest();
            }

            try
            {
                await _orderItems.Update(orderItem);
                return Ok(orderItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrderItemExists(id))
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
        [Produces(typeof(OrderItem))]
        public async Task<IActionResult> PostOrderItem([FromBody] OrderItem orderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _orderItems.Add(orderItem);

            return CreatedAtAction("GetOrderItem", new { id = orderItem.OrderItemId }, orderItem);
        }

        [HttpDelete("{id}")]
        [Produces(typeof(OrderItem))]
        public async Task<IActionResult> DeleteOrderItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (! await OrderItemExists(id))
            {
                return NotFound();
            }

            await _orderItems.Remove(id);

            return Ok();
        }
    }
}
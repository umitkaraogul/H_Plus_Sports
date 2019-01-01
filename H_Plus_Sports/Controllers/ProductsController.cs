using System.Threading.Tasks;
using H_Plus_Sports.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using H_Plus_Sports.Models;

namespace H_Plus_Sports.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _products;

        public ProductsController(IProductRepository products)
        {
            _products = products;
        }

        private async Task<bool> ProductExists(string id)
        {
            return await _products.Exists(id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Product>))]
        public IActionResult GetProduct()
        {
            return new ObjectResult(_products.GetAll());
        }

        [HttpGet("{id}")]
        [Produces(typeof(Product))]
        public async Task<IActionResult> GetProduct([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        [Produces(typeof(Product))]
        public async Task<IActionResult> PutProduct([FromRoute] string id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            try
            {
                await _products.Update(product);
                return Ok(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(id))
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
        [Produces(typeof(Product))]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _products.Add(product);
            }
            catch (DbUpdateException)
            {
                if (!await ProductExists(product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        [HttpDelete("{id}")]
        [Produces(typeof(Product))]
        public async Task<IActionResult> DeleteProduct([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await ProductExists(id))
            {
                return NotFound();
            }

            await _products.Remove(id);

            return Ok();
        }
    }
}
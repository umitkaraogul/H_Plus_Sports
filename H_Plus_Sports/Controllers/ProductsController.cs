using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using H_Plus_Sports.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSportsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly H_Plus_SportsContext _context;
        public ProductsController(H_Plus_SportsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProduct()
        {
            return new ObjectResult(_context.Product);
        }

        [HttpGet("{id}",Name = "GetProduct")]
        public async Task<IActionResult> GetProduct([FromRoute] string id)
        {
            var product = await _context.Product.SingleOrDefaultAsync(m => m.ProductId==id);
            
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("getProduct", new {id = product.ProductId},value: product);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
        {
            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();
           
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] string id)
        {
            var product = await _context.Product.SingleOrDefaultAsync(m => m.ProductId == id);

            _context.Product.Remove(product);
            
            await _context.SaveChangesAsync();
            
            return Ok(product);
        }
    }
}
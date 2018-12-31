using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HPlusSportsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        public ProductsController()
        {

        }

        [HttpGet]
        public IActionResult GetProduct()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct([FromRoute] int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PostProduct([FromBody] Object obj)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult PutProduct([FromRoute] int id, [FromBody] Object obj)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            return Ok();
        }
    }
}
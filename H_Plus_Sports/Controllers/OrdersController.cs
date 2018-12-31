using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HPlusSportsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        public OrdersController()
        {

        }

        [HttpGet]
        public IActionResult GetOrder()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder([FromRoute] int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PostOrder([FromBody] Object obj)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult PutOrder([FromRoute] int id, [FromBody] Object obj)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder([FromRoute] int id)
        {
            return Ok();
        }
    }
}
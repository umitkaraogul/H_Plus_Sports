using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace H_Plus_Sports.Controllers
{
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : Controller
    {
        public CustomersController()
        {
            
        }
        
        [HttpGet]
        public IActionResult GetCustomer()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer([FromRoute] int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PostCustomer([FromBody] Object obj)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult PutCustomer([FromRoute] int id, [FromBody] Object obj)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer([FromRoute] int id)
        {
            return Ok();
        }

    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiFilter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetValues()
        {
            throw new NotImplementedException();
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetValue([FromRoute]int id)
        {
            Console.WriteLine("GetValue执行中...............");
            return Ok(id);
        }
    }
}

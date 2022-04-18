using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zack.EventBus;

namespace WebApiRabbitMQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IEventBus _eventBus;

        public ValuesController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        [HttpGet]
        public IActionResult Get()
        {
            _eventBus.Publish("UserCreated", "michaelshen");
            return Ok();
        }
    }
}

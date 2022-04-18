using FluentValidationWebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateStudent([FromBody]StudentAddDto student)
        {
            return Ok(student);
        }
    }
}

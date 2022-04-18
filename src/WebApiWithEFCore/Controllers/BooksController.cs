using EFCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiWithEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public BooksController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _dbContext.Books;
            return Ok(books);
        }
        [HttpPost]
        public IActionResult CreateBook([FromBody]Book book)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            return Ok(book);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace WebApiFilter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [NotTransaction]
        public async Task<IActionResult> CreateBook(Book book)
        {

            using(TransactionScope ts=new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {//异步代码中需要传一个TransactionScopeAsyncFlowOption.Enabled
                _context.Add(new Book
                {
                    AuthorName = "michaelshen",
                    Price = 100,
                    PublishDate = DateTime.Now,
                    Title = "mybook"
                });
                await _context.SaveChangesAsync();//一个事物
                _context.Persons.Add(new Person
                {
                    Name = null
                });
                _context.SaveChanges();//一个事物
                ts.Complete();
            }
            return Ok(book);
        }

    }
}

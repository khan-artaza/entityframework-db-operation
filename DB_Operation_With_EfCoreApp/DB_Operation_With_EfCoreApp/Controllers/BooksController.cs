using DB_Operation_With_EfCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DB_Operation_With_EfCoreApp.Controllers
{
    [Route("api/books/add")]
    [ApiController]
    public class BooksController(AppDbContext _appDbContext) : ControllerBase
    {
        [HttpPost("one")]
        public async Task<IActionResult> AddBook([FromBody] Book model)
        {
            _appDbContext.Books.Add(model);

            await _appDbContext.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AddBulkBooks([FromBody] List<Book> books)
        {
             _appDbContext.Books.AddRange(books);
            await _appDbContext.SaveChangesAsync();

            return Ok(books);
        }


    }
}

using DB_Operation_With_EfCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DB_Operation_With_EfCoreApp.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController(AppDbContext _appDbContext) : ControllerBase
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddBook([FromBody] Book model)
        {
            _appDbContext.Books.Add(model);

            await _appDbContext.SaveChangesAsync();

            return Ok(model);
        }
    }
}

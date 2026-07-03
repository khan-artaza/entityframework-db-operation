using DB_Operation_With_EfCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DB_Operation_With_EfCoreApp.Controllers
{
    [Route("api/books/add")]
    [ApiController]
    public class BooksController(AppDbContext _appDbContext) : ControllerBase
    {
        [HttpPost("one")]
        public async Task<IActionResult> AddBook([FromBody] Book model)
        {
            //create a new Author object (not saved to db yet)
            var author = new Author() { Name = "Paulo Coelo", Email = "pc@gmail.com" };
            
            //this "model" is Book type so this author object is assigned to book table's navigation property Author? Author{get;set;}
            model.Author = author;

            //add only the book , ef core sees that book is connected to new author object
            _appDbContext.Books.Add(model);

            /*when save changes runs,
             * 1.Insert the author first
             * 2. gets the generated author id,
             * 3.automatically puts that author id into Book.AuthorId */
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

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id,[FromBody] Book bookObj)
        {
            var book = _appDbContext.Books.FirstOrDefault(x => x.Id == id);

            if(book == null)
            {
                return NotFound();
            }

            book.Title = bookObj.Title;
            book.Description = bookObj.Description;
            book.LanguageId = bookObj.LanguageId;

            await _appDbContext.SaveChangesAsync();
            return Ok(bookObj);
        }
    }
}

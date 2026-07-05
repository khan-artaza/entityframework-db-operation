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

        //this method hits db only once that's why this technique is optimized way to update record in db but
        //there is one drawback with this technique is we need to give each an every fiels of record from the
        //frontend eventhough we dont need to update all fields, we just need to update only few fields.
        //if we dont give all fields from the frontend then, that fields will become changes to null and lose its previous value.
        [HttpPut("updates")]
        public async Task<IActionResult> UpdateBookInSingleQuery([FromBody] Book bookObj)
        {
            _appDbContext.Books.Update(bookObj);

            await _appDbContext.SaveChangesAsync();

            return Ok(bookObj);
        }

        [HttpPut("updatebulk")]
        public async Task<IActionResult> UpdateBulk()
        {
            await _appDbContext.Books
                .Where(r => r.NoOfPages == 59) //if u want to give condition to update on rows with particular conditions
                .ExecuteUpdateAsync(r => r //if u dont give Where conditions it update all rows
                    .SetProperty(c => c.Description, "updated description 2")
                    .SetProperty(c => c.Title, c => c.Title + "updated 2")
                );

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOneById([FromRoute] int id)
        {
            //with double db hits
            var book = _appDbContext.Books.FirstOrDefault(r => r.Id == id); //hit 1

            if(book == null)
            {
                return NotFound();
            }

            _appDbContext.Books.Remove(book);

            await _appDbContext.SaveChangesAsync(); //hit 2

            return Ok();

            //with single hit
            /*var bookInstance = new Book() { Id = id };
            _appDbContext.Entry(bookInstance).State = EntityState.Deleted;*/
        }

        [HttpDelete("deleteBulk")]
        public async Task<IActionResult> DeleteInBulk()
        {
            var totalNumOfRowsDeleted = await _appDbContext.Books.Where(r => r.Id < 13).ExecuteDeleteAsync();

            return Ok();
        }

        [HttpGet("allBooks")]
        public async Task<IActionResult> ShowAllBooks()
        {
            /*
                Adding .AsNoTracking() to your query tells Entity Framework: "Just give me the data. Do not track these objects."
                Why we use it :
                Performance. Speed: Queries execute faster because EF skips the extra work of setting up tracking information.
                It is used when No tracking needed; we are just displaying the data to the UI.
             */
            var books = await _appDbContext.Books.AsNoTracking().ToListAsync();

            return Ok(books);
        }

        //getting data from more than one table that are connected by navigation property i.e like join in sql
        [HttpGet("allbookDetails")]
        public async Task<IActionResult> ShowAllBookWithAuthorDetails()
        {
            var bookdetails = _appDbContext.Books.AsNoTracking().Select(x => new
            {
                Title = x.Title,
                Language = x.Language != null ? x.Language.Title : "NA",
                Author = x.Author != null ? x.Author.Name : "NA"
            });

            return Ok(bookdetails);
        }

        //getting data of main table with related table data using eager loading technique
        [HttpGet("getbooksinfo")]
        public async Task<IActionResult> GetBookInfo()
        { 

            //EAGER LOADING - Eager Loading is a data retrieval technique in EF Core that loads the primary entity and its related entities in a single SQL query.
            //It is implemented using Include() and ThenInclude() methods,
            //which instruct EF Core to automatically join related tables during query execution.

            var bookDetails = await _appDbContext.Books.AsNoTracking()
                                                   .Include(t => t.Language)
                                                   .Include(t => t.Author)
                                                   .ToListAsync();

            return Ok(bookDetails);
        }
    }
}

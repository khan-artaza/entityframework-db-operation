// We need AppDbContext because it contains our database connection
using DB_Operation_With_EfCoreApp.Data;

// Basic ASP.NET Core classes
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// Namespace is like a folder for related classes
namespace DB_Operation_With_EfCoreApp.Controllers
{
    // All requests starting with: "api/currencies" will come to this controller
    [Route("api/currencies")]
    [ApiController]// Tells ASP.NET that this is a Web API controller
    public class CurrencyController : ControllerBase // ControllerBase gives us methods like: Ok(), NotFound(), BadRequest() etc.
    {
        private readonly AppDbContext _dbcontext; // Variable to store database connection, readonly means once assigned, it cannot be changed

        // Constructor
        // ASP.NET automatically gives us AppDbContext object
        // through Dependency Injection (DI)
        public CurrencyController(AppDbContext dbcontext)
        {
            // Store the received database object
            // inside our private variable
            _dbcontext = dbcontext;
        }

        // This method handles GET requests
        // URL: GET /api/currencies
        [HttpGet("")]

        // IActionResult means this method returns an HTTP response (200, 404, 400, etc.)
        public IActionResult GetAllCurrencies()
        {
            // Go to CurrencyTypes table
            // Fetch all records
            // Convert them into a List
            var result = _dbcontext.CurrencyTypes.ToList();

            // Return status code 200 (OK) along with the data as JSON
            return Ok(result);
        }
    }
}
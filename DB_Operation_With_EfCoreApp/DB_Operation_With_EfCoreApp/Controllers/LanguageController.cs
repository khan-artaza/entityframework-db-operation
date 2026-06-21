using DB_Operation_With_EfCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DB_Operation_With_EfCoreApp.Controllers
{
    [Route("api/languages")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly AppDbContext _dbcontext;

        public LanguageController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllLanguage()
        {
            var result = await _dbcontext.Languages.ToListAsync();

            //another way
            //var result = await (from languages in _dbcontext.Languages
                               // select languages).ToListAsync();

            return Ok(result);
        }
    }
}

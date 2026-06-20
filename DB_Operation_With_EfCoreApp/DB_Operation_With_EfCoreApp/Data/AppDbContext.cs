using Microsoft.EntityFrameworkCore;
namespace DB_Operation_With_EfCoreApp.Data
{
    public class AppDbContext : DbContext
    {
        //making a constructor to pass the options to the base class

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
 
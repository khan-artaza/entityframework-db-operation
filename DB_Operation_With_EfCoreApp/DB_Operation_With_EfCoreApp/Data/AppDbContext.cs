using Microsoft.EntityFrameworkCore;
namespace DB_Operation_With_EfCoreApp.Data
{
    public class AppDbContext : DbContext
    {
        //making a constructor to pass the options to the base class

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } //creating a DbSet property for the Book entity to represent the Books table in the database

        public DbSet<Language> Languages { get; set; } //creating a DbSet property for the Language entity to represent the Languages table in the database

        public DbSet<BookPrice> BookPrices { get; set; } 

        public DbSet<CurrencyType> CurrencyTypes { get; set; }
    }
}
 
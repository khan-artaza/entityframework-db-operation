using Microsoft.EntityFrameworkCore;
namespace DB_Operation_With_EfCoreApp.Data
{
    public class AppDbContext : DbContext
    {
        //making a constructor to pass the options to the base class

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //overriding the OnModelCreating method to configure the model using Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyType>().HasData(
                new CurrencyType { Id = 1, Currency = "USD", Description = "US Dollar" },
                new CurrencyType { Id = 2, Currency = "EUR", Description = "Euro" },
                new CurrencyType { Id = 3, Currency = "GBP", Description = "British Pound" },
                new CurrencyType { Id = 4, Currency = "INR", Description = "Indian Rupees"},
                new CurrencyType { Id = 5, Currency = "INR", Description = "From India" }
                );//seeding the CurrencyType table with initial data using the HasData method

            modelBuilder.Entity<Language>().HasData(
                new Language { Id = 1, Title = "English", Description = "English Language" },
                new Language { Id = 2, Title = "Spanish", Description = "Spanish Language" },
                new Language { Id = 3, Title = "Hindi", Description = "Hindi Language" },
                new Language { Id = 4, Title = "Urdu", Description = "Urdu Language" }
                );//seeding the Language table with initial data using the HasData method
        }

        public DbSet<Book> Books { get; set; } //creating a DbSet property for the Book entity to represent the Books table in the database

        public DbSet<Language> Languages { get; set; } //creating a DbSet property for the Language entity to represent the Languages table in the database

        public DbSet<BookPrice> BookPrices { get; set; } 

        public DbSet<CurrencyType> CurrencyTypes { get; set; }

        public DbSet<Author> Authors { get; set; }
    }
}
 
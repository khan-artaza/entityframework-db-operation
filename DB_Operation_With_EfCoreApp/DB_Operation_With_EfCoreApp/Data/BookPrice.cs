namespace DB_Operation_With_EfCoreApp.Data
{
    public class BookPrice
    {
        public int Id { get; set; }
        public int BookId { get; set; } //adding foreign key
        public double Amount { get; set; }
        public int CurrencyId { get; set; } //adding foreign key

        public CurrencyType CurrencyType { get; set; } //adding navigation to currency type class
        public Book Book { get; set; } //adding navigation property to the book class
    }
}

namespace DB_Operation_With_EfCoreApp.Data
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }


        //one-to-many relationship setup
        public ICollection<Book> Books { get; set; }
    }
}

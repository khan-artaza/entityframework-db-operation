using System.Text.Json.Serialization;

namespace DB_Operation_With_EfCoreApp.Data
{
    public class Language //creating a Language class to represent the Language table in the database
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Book> Books { get; set; } //adding a collection of books to the language class to establish a relationship between the two entities (book and language)
    }
}

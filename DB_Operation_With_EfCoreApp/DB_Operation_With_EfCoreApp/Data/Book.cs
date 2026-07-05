using System.Text.Json.Serialization;

namespace DB_Operation_With_EfCoreApp.Data
{
    public class Book //creating a Book class to represent the Book table in the database
    {
        public int Id{ get; set; }
        public string Title{ get; set; }
        public string Description{ get; set; }
        public int NoOfPages{ get; set; }
        public bool IsActive{ get; set; }

        public DateTime CreatedOn { get; set; }

        public int LanguageId { get; set; } //adding a foreign key property to the Book class to establish a relationship between the two entities (book and language)
        public int? AuthorId { get; set; } // adding a foreign key to author class (table)

        //[JsonIgnore] //do NOT return this directly in Api responses oterwise JSON goes Book -> Author -> Book -> Author -> ...
        public Author ? Author { get; set; } //adding a navigation property to author table.
        public Language ? Language { get; set; } //adding a navigation property to the Language class to establish a relationship between the two entities (book and language)
    }
}

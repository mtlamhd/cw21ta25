namespace CW21Ta23.Domain.Entities;

    public class Category : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }

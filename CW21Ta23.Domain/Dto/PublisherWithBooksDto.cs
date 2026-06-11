namespace CW21Ta23.Domain.Dto;

    public class PublisherWithBooksDto
    {
        public string PublisherName { get; set; }

        public string PublisherCity { get; set; }

        public int BooksCount { get; set; }

        public List<string> BookTitles { get; set; } = new List<string>();
    }

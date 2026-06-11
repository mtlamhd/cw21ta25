namespace CW21Ta23.Domain.Dto;

public class BookWithDetailsDto
{
    public string Title { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public string AuthorName { get; set; }
    public string CategoryName { get; set; }
    public string PublisherName { get; set; }

    public List<string> Tags { get; set; } = new();
}
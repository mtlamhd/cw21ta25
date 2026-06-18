namespace CW21Ta23.Domain.Dto;

public class CreateBookDtoo
{
    public string Title { get; set; }
    
    public decimal Price { get; set; }

    public int PublishYear { get; set; }

    public int AuthorId { get; set; }

    public int CategoryId { get; set; }

    public int PublisherId { get; set; }

    public int Stock { get; set; }
}
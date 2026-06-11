namespace CW21Ta23.Domain.Entities;

public class Book : BaseEntity
{
    
    public string Title { get; set; }
    
    public decimal Price { get; set; }
    
    public int PublishYear { get; set; }
    
    public int Stock { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public int AuthorId { get; set; }
    
    public Author Author { get; set; }
    
    public int CategoryId { get; set; }

    public Category Category { get; set; }
    
    public int PublisherId { get; set; } 

    public Publisher Publisher { get; set; }

    public List<Tag> Tags { get; set; } = new List<Tag>();
}
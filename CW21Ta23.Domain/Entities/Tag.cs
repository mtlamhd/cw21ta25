namespace CW21Ta23.Domain.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; }
    
    public List<Book> Books { get; set; } = new List<Book>();

    public DateTime CreatedAt { get; set; }

}
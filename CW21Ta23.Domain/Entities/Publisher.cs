namespace CW21Ta23.Domain.Entities;

public class Publisher : BaseEntity
{
    public string Name { get; set; }

    public string City { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public List<Book> Books { get; set; } = new List<Book>();
}
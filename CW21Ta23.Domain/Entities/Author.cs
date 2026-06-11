namespace CW21Ta23.Domain.Entities;

public class Author : BaseEntity
{
    public string FullName { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public string Country { get; set; }

    public List<Book> Books { get; set; } = new List<Book>();
}
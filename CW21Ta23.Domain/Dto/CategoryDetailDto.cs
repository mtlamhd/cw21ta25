namespace CW21Ta23.Domain.Dto;

public class CategoryDetailDto
{
    public string CategoryName { get; set; }

    public string Description { get; set; }

    public int BooksCount { get; set; }
    
    public List<string> Books { get; set; }
}
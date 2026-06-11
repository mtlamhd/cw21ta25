namespace CW21Ta23.Domain.Dto;

public class AuthorWithExpensiveBookReportDto
{
    public string Name { get; set; }
    public string BookName { get; set; }
    public decimal ExpensiveBookPrice { get; set; }
}
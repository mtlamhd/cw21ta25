namespace CW21Ta23.Domain.Dto;

public class PublisherStatisticsDto
{
    public string PublisherName { get; set; }

    public int BooksCount { get; set; }

    public int TotalStock { get; set; }

    public decimal AveragePrice { get; set; }
}

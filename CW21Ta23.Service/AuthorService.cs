using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.Entities;
using CW21Ta23.Domain.RepositoryInterFaces;
using CW21Ta23.Domain.ServiceIntefaces;

namespace CW21Ta23.Service;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookRepository _bookRepository;
    public AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository)
    {
        _authorRepository = authorRepository;
        _bookRepository = bookRepository;
    }

    public async Task<List<AuthorWithBooksCountDto>> GetAuthorWithBooksCountAsync()
    {
        var authors = await _authorRepository.GetAllAsync();
        var books = await _bookRepository.GetAllAsync();
                                                                                                
        return authors.Select(a => new AuthorWithBooksCountDto
        {
            Name = a.FullName,
            Count = books.Count(b => b.AuthorId == a.Id)
        }).ToList();
    }
    public async Task<List<AuthorTotalPriceDto>> GetAuthorTotalPrices()
    {
        var authors = await _authorRepository.GetAllAsync();
        var books = await _bookRepository.GetAllAsync();

        return  authors.Select(a => new AuthorTotalPriceDto
        {
            FullName = a.FullName,
            TotalPrice = books
                .Where(b => b.AuthorId == a.Id)
                .Sum(b => b.Price)
        }).ToList();
    }

    public async Task<List<AuthorWithMoreThan2BooksDto>> GetAuthorWithMoreThan2BooksAsync()
    {
        var authors = await _authorRepository.GetAllAsync();
        var books = await _bookRepository.GetAllAsync();

        return authors
            .Where(a => books.Count(b => b.AuthorId == a.Id) >= 2)
            .Select(a => new AuthorWithMoreThan2BooksDto
            {
                Name = a.FullName,
                Count = books.Count(b => b.AuthorId == a.Id)
            })
            .ToList();
    }

    public async Task<List<AuthorReportDto>> GetAuthorReportDto(decimal minAveragePrice)
    {
        var authors = await _authorRepository.GetAllAsync();
        var books = await _bookRepository.GetAllAsync();

        return authors
            .Select(a => new AuthorReportDto
            {
                Name = a.FullName,
                AvgPrice = books
                    .Where(b => b.AuthorId == a.Id)
                    .Select(b => b.Price)
                    .DefaultIfEmpty(0)
                    .Average()
            })
            .Where(a => a.AvgPrice > minAveragePrice)
            .ToList();
    }

    public async Task<List<AuthorWithExpensiveBookReportDto>> GetAuthorWithExpensiveBookReport()
    {
        var authors = await _authorRepository.GetAllAsync();
        var books = await _bookRepository.GetAllAsync();
        var groupedBooks = books.GroupBy(b => b.AuthorId);

        return groupedBooks
            .Select(g =>
            {
                var mostExpensiveBook = g
                    .OrderByDescending(b => b.Price)
                    .First();

                var author = authors.First(a => a.Id == g.Key);

                return new AuthorWithExpensiveBookReportDto
                {
                    Name = author.FullName,
                    BookName = mostExpensiveBook.Title,
                    ExpensiveBookPrice = mostExpensiveBook.Price
                };
            })
            .ToList();
    }
    
    
    
    
}
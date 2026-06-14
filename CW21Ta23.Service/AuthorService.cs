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

    public async Task<List<AuthorWithBooksCountDto>> GetAuthorsWithBooksCount()
    {
        var authors = await _authorRepository.GetAuthorsWithBooksCount();
        return authors;
    }

    public async Task<AuthorWithBookDto> GetAuthorByIdAsync(int authorId)
    {
        var author = await _authorRepository.GetAuthorByIdAsync(authorId);

        if (author == null)
            throw new Exception("author not found");

        return author;
    }

    public async Task<List<AuthorWithBooksCountDto>> GetAuthorsWithMoreThanTwoBooksAsync()
    {
        return await _authorRepository.GetAuthorsWithMoreThanTwoBooksAsync();
    }

    public async Task<List<AuthorWithBookDto>> GeAuthorsByName(string authorName)
    {
        if(string.IsNullOrWhiteSpace(authorName))
            throw new Exception("authorName is Required");
        
        var authors = await _authorRepository.GeAuthorsByName(authorName);
        
        return authors;
            
    }

    public async Task<int> CreateAuthor(CreateAuthorDto dto)
    {
        if(string.IsNullOrWhiteSpace(dto.FullName))
            throw new Exception("Fullname is Required");
        if (dto.FullName.Length > 100)
            throw new Exception("FullName is too long");
        var author = new Author
        {
            FullName = dto.FullName,
            BirthDate = dto.BirthDate ?? default,
            Country = dto.Country
        };
        await _authorRepository.AddAsync(author);
        return author.Id;
    }

    public async Task<bool> DeleteAuthorAsync(int authorId)
    {
        var author = await _authorRepository.GetByIdWithBookAsync(authorId);

        if (author == null)
            throw new Exception("author not found");

        if (author.Books.Any())
            throw new Exception("cannot delete author because it has books");

        await _authorRepository.HardDeleteAsync(author);

        return true;
    }
    
    public async Task<bool> UpdateAuthorAsync(int id, UpdateAuthorDto dto)
    {
        var author = await _authorRepository.FindByIdAsync(id);

        if (author == null)
            throw new Exception("author not found");

        if (string.IsNullOrWhiteSpace(dto.FullName))
            throw new Exception("full name is required");

        if (dto.FullName.Length > 100)
            throw new Exception("full name is too long");

        author.FullName = dto.FullName;
        author.BirthDate = dto.BirthDate ?? default;
        author.Country = dto.Country;

        await _authorRepository.UpdateAsync(author);

        return true;
    }
    
    
}
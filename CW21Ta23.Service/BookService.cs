using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.Entities;
using CW21Ta23.Domain.RepositoryInterFaces;
using CW21Ta23.Domain.ServiceIntefaces;

namespace CW21Ta23.Service;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPublisherRepository _publisherRepository;

    public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository,
        ICategoryRepository categoryRepository, IPublisherRepository publisherRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _categoryRepository = categoryRepository;
        _publisherRepository = publisherRepository;
    }

    public async Task<bool> BookExists(string title)
    {
        var books = await _bookRepository.QueryAsync(b => b.Title == title);
        return books.Any();
    }

    public async Task<BookWithTotalPriceExpensiveDto?> GetExpensiveBook()
    {
        var books = await _bookRepository.GetAllAsync();

        return books
            .OrderByDescending(b => b.Price)
            .Select(b => new BookWithTotalPriceExpensiveDto
            {
                Title = b.Title,
                Price = b.Price
            })
            .FirstOrDefault();
    }

    public async Task<List<BooksWithMoreThanAvgDto>> GetBooksWithMoreThanAvg()
    {
        var books = await _bookRepository.GetAllAsync();

        if (!books.Any())
            throw new ArgumentNullException("books not exist");

        var avg = books.Average(b => b.Price);

        return books
            .Where(b => b.Price > avg)
            .Select(b => new BooksWithMoreThanAvgDto
            {
                Name = b.Title,
                Price = b.Price
            })
            .ToList();
    }

    public async Task<List<BooksWithPaginationDto>> GetBooksWithPagination(int pageNumber = 1, int pageSize = 10)
    {
        var page = (pageNumber - 1) * pageSize;
        var books = await _bookRepository.GetAllAsync();
        if (!books.Any())
            throw new ArgumentNullException("books not exist");
        return books.Skip(page).Take(pageSize).Select(b => new BooksWithPaginationDto
        {
            Title = b.Title,
            AuthorId = b.AuthorId,
            PublishYear = b.PublishYear,
            CategoryId = b.CategoryId,
            Price = b.Price,
            Stock = b.Stock
        }).ToList();
    }

    public async Task<string> UpdateBookStock(UpdateBookStockDto dto)
    {
        if (dto.NewStock < 0)
            return "stock can not be negative";

        var book = await _bookRepository.FindByIdAsync(dto.BookId, tracking: true);

        if (book == null)
            return "book not found";

        book.Stock = dto.NewStock;

        await _bookRepository.UpdateAsync(book);

        return "update successfully";
    }

    public async Task<string> SellBookAsync(SellBookDto dto)
    {
        if (dto.BookId < 0)
            throw new Exception("Id can not negative");

        if (dto.Count <= 0)
            throw new Exception("invalid Count");

        var book = await _bookRepository.FindByIdAsync(dto.BookId, true);

        if (dto.Count > book.Stock)
            return "mojudi kafi nis";

        book.Stock -= dto.Count;

        await _bookRepository.UpdateAsync(book);

        return $"succesfully : {book.Stock}";
    }

    public async Task<string> DeleteBookAsync(DeleteBookDto dto)
    {
        if (dto.BookId <= 0)
            return "invalid ID";

        var book = await _bookRepository.FindByIdAsync(dto.BookId, true);

        if (book == null)
            return "book not found";

        await _bookRepository.HardDeleteAsync(book);

        var remainingBooksCount = (await _bookRepository.GetAllAsync()).Count;

        return $"successfully deleted. remaining books: {remainingBooksCount}";
    }

    public async Task<List<BookReportTotalDto>> GetBookReportTotal()
    {
        var books = await _bookRepository.GetAllAsync();
        var authors = await _authorRepository.GetAllAsync();
        var categories = await _categoryRepository.GetAllAsync();
        var publishers = await _publisherRepository.GetAllAsync();

        return books.Select(b => new BookReportTotalDto
        {
            Title = b.Title,
            Price = b.Price,
            Stock = b.Stock,
            AuthorName = authors.First(a => a.Id == b.AuthorId).FullName,
            CategoryName = categories.First(c => c.Id == b.CategoryId).Title,
            PublisherName = publishers.First(p => p.Id == b.PublisherId).Name,
            PublisherCity = publishers.First(p => p.Id == b.PublisherId).City
        }).ToList();
    }

    public async Task<List<BookWithTotalPriceExpensiveDto>> GetBookWithMoreThanStockAndAvg()
    {
        var books = await _bookRepository.GetAllAsync();
        var avgPrice = books.Average(b => b.Price);
        return books.Where(b => b.Stock > 0).Where(b=>b.Price>avgPrice)
            .OrderByDescending(b=>b.Price)
            .Select(b => new BookWithTotalPriceExpensiveDto
        {
            Title    = b.Title,
            Price = b.Price
        }).ToList();
    }
    
    public async Task<List<BookWithDetailsDto>> GetAllBooks()
    {
        return await _bookRepository.GetAllBooksWithDetailsAsync();
    }

    public async Task<BookWithDetailTotaDto?> GetBookWithDetailsAsync(int bookId)
    {
        var book = await _bookRepository.GetBookWithDetailsAsync(bookId);
        
        if(book == null)
            throw new ArgumentNullException("book not found");
        return book;

    }
    

}
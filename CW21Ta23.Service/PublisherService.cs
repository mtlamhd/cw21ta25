using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.Entities;
using CW21Ta23.Domain.RepositoryInterFaces;
using CW21Ta23.Domain.ServiceIntefaces;

namespace CW21Ta23.Service;

public class PublisherService : IPublisherService
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IBookRepository _bookRepository;

    public PublisherService(IPublisherRepository publisherRepository, IBookRepository bookRepository)
    {
        _publisherRepository = publisherRepository;
        _bookRepository = bookRepository;
    }

    public async Task<List<PublisherWithBooksDto>> GetPublishersWithBooksAsync()
    {
        var publishers =await _publisherRepository.GetAllAsync();
        var books =await _bookRepository.GetAllAsync();
        return publishers.Select(p =>
        {
            var publisherBooks = books.Where(b => b.PublisherId == p.Id).ToList();

            return new PublisherWithBooksDto
            {
                PublisherName = p.Name,
                PublisherCity = p.City,
                BooksCount = publisherBooks.Count,
                BookTitles = publisherBooks.Select(b => b.Title).ToList()
            };
        }).ToList();
    }

    public async Task<List<PublisherWithBookCountDto>> GetPublishersWithBooksByPublisherAsync()
    {
        var books = await _bookRepository.GetAllAsync(); 
        var publishers = await _publisherRepository.GetAllAsync();
        return publishers.Select(p => new PublisherWithBookCountDto
        {
            
            PublisherName = p.Name,
            BooksCount = books.Count(b=>b.PublisherId == p.Id) 
        }).Where(p=>p.BooksCount>2).ToList();
    }

    public async Task<List<PublisherStatisticsDto>> GetPublisherStaticAsync()
    {
        var books = await _bookRepository.GetAllAsync(); 
        var publishers = await _publisherRepository.GetAllAsync();
        return publishers.Select(p => new PublisherStatisticsDto
        {
            PublisherName = p.Name,
            AveragePrice = books
                .Where(b => b.PublisherId == p.Id)
                .Select(b => b.Price)
                .DefaultIfEmpty(0)
                .Average(),
            BooksCount = books.Count(b=>b.PublisherId == p.Id),
            TotalStock = books.Where(b=>b.PublisherId==p.Id).Sum(b=>b.Stock)
            
        }).ToList();
    }

    public async Task<List<PublisherWithExpensiveBookDto>> GetMostExpensiveBookPerPublisher()
    {
        var books = await _bookRepository.GetAllAsync(); 
        var publishers = await _publisherRepository.GetAllAsync();

        return publishers
            .Select(p =>
            {
                var publisherBooks = books.Where(b => b.PublisherId == p.Id);

                if (!publisherBooks.Any())
                    return null;

                var maxPrice = publisherBooks.Max(b => b.Price);

                var book = publisherBooks.First(b => b.Price == maxPrice);

                return new PublisherWithExpensiveBookDto
                {
                    PublisherName = p.Name,
                    Price = book.Price,
                    BookName = book.Title
                };
            })
            .Where(x => x != null)
            .ToList();
    }

    public async Task<string> ChangePublisherOfBookAsync(ChangePublisherOfBookDto dto)
    {
        if(dto.PublisherId<=0)
            return "invalid publisher";
        
        if(dto.BookId<=0)
            return "invalid book";
        
        var publisher = await _publisherRepository.FindByIdAsync(dto.PublisherId,true);
        
        if(publisher == null)
            return "invalid publisher";
        
        var book=await _bookRepository.FindByIdAsync(dto.BookId,true);
        
        if(book == null)
            return "invalid book";
        
        book.PublisherId = dto.PublisherId;
        
       await _bookRepository.UpdateAsync(book);
       
       return $"success - {book.PublisherId}";
       
    }
    
    
    
    
    
    
    
    
}
using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.Entities;
using CW21Ta23.Domain.RepositoryInterFaces;
using CW21Ta23.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CW21Ta23.Infrastructure.Repositiries;

public class BookRepository : GenericRepository<Book> , IBookRepository
{
    public BookRepository(AppDbContext context) : base(context)
    {
      
    }

    public async Task<List<BookWithDetailsDto>> GetAllBooksWithDetailsAsync()
    {
        return await _context.Books.AsNoTracking()
            .Select(b => new BookWithDetailsDto
            {
                Title = b.Title,
                Price = b.Price,
                Stock = b.Stock,
                AuthorName = b.Author.FullName,
                CategoryName = b.Category.Title,
                PublisherName = b.Publisher.Name,
                Tags = b.Tags.Select(t => t.Name).ToList()
            }).ToListAsync();
    }

    public async Task<BookWithDetailTotaDto?> GetBookWithDetailsAsync(int bookId)
    {
        return await _context.Books.AsNoTracking()
            .Where(b => b.Id == bookId)
            .Select(b => new BookWithDetailTotaDto
            {
                
                Title = b.Title,
                Price = b.Price,
                Stock = b.Stock,
                AuthorName = b.Author.FullName,
                CategoryName = b.Category.Title,
                PublisherName = b.Publisher.Name,
                PublishYear =  b.PublishYear,
                Tags = b.Tags.Select(t => t.Name).ToList(),
                
            }).FirstOrDefaultAsync();
        
         
    }
    
    public async Task<Book?> FindWithTagsAsync(int bookId)
    {
        return await _context.Books
            .Include(b => b.Tags)
            .FirstOrDefaultAsync(b => b.Id == bookId);
    }

    public async Task<BookWithDetailTotaDto?> GetBookByName(string bookName)
    {
        return await _context.Books.AsNoTracking()
            .Where(b => b.Title.ToLower() == bookName.ToLower())
            .Select(b => new BookWithDetailTotaDto
            {
                Title = b.Title,
                Price = b.Price,
                Stock = b.Stock,
                AuthorName = b.Author.FullName,
                CategoryName = b.Category.Title,
                PublisherName = b.Publisher.Name,
                PublishYear = b.PublishYear,
                Tags = b.Tags.Select(t => t.Name).ToList()
            }).FirstOrDefaultAsync();
    }

    public async Task<List<BookModelDto>> GetBookByCategory(int categoryId)
    {
        return await _context.Books.AsNoTracking()
            .Where(b => b.CategoryId == categoryId)
            .Select(b => new BookModelDto
                {
                    Title = b.Title,
                    Price = b.Price,
                    Stock = b.Stock,
                    PublishYear = b.PublishYear,
                    AuthorName = b.Author.FullName,
                    PublisherName = b.Publisher.Name,
                    CategoryName = b.Category.Title,
                    Tags = b.Tags.Select(t=>t.Name).ToList()

                }

            ).ToListAsync();
    }
    public async Task<List<BookModelDto>> GetBookByAuthor(int authorId)
    {
        return await _context.Books.AsNoTracking()
            .Where(b => b.AuthorId == authorId)
            .Select(b => new BookModelDto
                {
                    Title = b.Title,
                    Price = b.Price,
                    Stock = b.Stock,
                    PublishYear = b.PublishYear,
                    AuthorName = b.Author.FullName,
                    PublisherName = b.Publisher.Name,
                    CategoryName = b.Category.Title,
                    Tags = b.Tags.Select(t=>t.Name).ToList()

                }

            ).ToListAsync();
    }
    
    public async Task<List<BookModelDto>> GetBookByPublisher(int publisherId)
    {
        return await _context.Books.AsNoTracking()
            .Where(b => b.Publisher.Id == publisherId)
            .Select(b => new BookModelDto
                {
                    Title = b.Title,
                    Price = b.Price,
                    Stock = b.Stock,
                    PublishYear = b.PublishYear,
                    AuthorName = b.Author.FullName,
                    PublisherName = b.Publisher.Name,
                    CategoryName = b.Category.Title,
                    Tags = b.Tags.Select(t=>t.Name).ToList()

                }

            ).ToListAsync();
    }

    public async Task<List<BookModelDto>> GetBooksByTag(int tagId)
    {
        return await _context.Books
            .AsNoTracking()
            .Where(b => b.Tags.Any(t => t.Id == tagId))
            .Select(b => new BookModelDto
            {
                Title = b.Title,
                Price = b.Price,
                Stock = b.Stock,
                PublishYear = b.PublishYear,
                AuthorName = b.Author.FullName,
                PublisherName = b.Publisher.Name,
                CategoryName = b.Category.Title,
                Tags = b.Tags.Select(t => t.Name).ToList()
            })
            .ToListAsync();
    }

    public async Task<List<BookModelDto>> GetBooksByPriceRange(decimal minPrice, decimal maxPrice)
    {
        return await _context.Books
            .AsNoTracking()
            .Where(b => b.Price >= minPrice &&
                        b.Price <= maxPrice)
            .Select(b => new BookModelDto
            {
                Title = b.Title,
                Price = b.Price,
                Stock = b.Stock,
                PublishYear = b.PublishYear,
                AuthorName = b.Author.FullName,
                PublisherName = b.Publisher.Name,
                CategoryName = b.Category.Title,
                Tags = b.Tags.Select(t => t.Name).ToList()
            })
            .ToListAsync();
    }
    
    public async Task<List<BookModelDto>> GetBooksPublishedAfterYear(int year)
    {
        return await _context.Books
            .AsNoTracking()
            .Where(b => b.PublishYear > year)
            .Select(b => new BookModelDto
            {
                Title = b.Title,
                Price = b.Price,
                Stock = b.Stock,
                PublishYear = b.PublishYear,
                AuthorName = b.Author.FullName,
                PublisherName = b.Publisher.Name,
                CategoryName = b.Category.Title,
                Tags = b.Tags.Select(t => t.Name).ToList()
            })
            .ToListAsync();
    }
}
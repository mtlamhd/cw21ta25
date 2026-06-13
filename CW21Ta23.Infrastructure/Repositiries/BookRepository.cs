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
}
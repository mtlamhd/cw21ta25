using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.Entities;
using CW21Ta23.Domain.RepositoryInterFaces;
using CW21Ta23.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CW21Ta23.Infrastructure.Repositiries;

public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<List<CategoryWithBookCountDto>> GetAllCategoriesWithBookCountAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .Select(c => new CategoryWithBookCountDto
            {
                CategoryName = c.Title,
                BooksCount = c.Books.Count()
            })
            .ToListAsync();
    }
    
    public async Task<CategoryDetailDto?> GetCategoryByIdAsync(int categoryId)
    {
        return await _context.Categories
            .AsNoTracking()
            .Where(c => c.Id == categoryId)
            .Select(c => new CategoryDetailDto
            {
                CategoryName = c.Title,
                Description = c.Description,
                BooksCount = c.Books.Count(),
                Books = c.Books.Select(b=>b.Title).ToList()
            })
            .FirstOrDefaultAsync();
    }
    public async Task<List<BookModelDto>> GetBooksByCategoryAsync(int categoryId)
    {
        return await _context.Books
            .AsNoTracking()
            .Where(b => b.CategoryId == categoryId)
            .Select(b => new BookModelDto
            {
                Title = b.Title,
                Price = b.Price,
                Stock = b.Stock,
                PublishYear = b.PublishYear,

                AuthorName = b.Author.FullName,
                PublisherName = b.Publisher.Name,

                Tags = b.Tags.Select(t => t.Name).ToList()
            })
            .ToListAsync();
    }
    
    public async Task<List<CategoryWithBookCountDto>> GetCategoriesWithAvailableBooksAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .Where(c => c.Books.Any(b => b.Stock > 0))
            .Select(c => new CategoryWithBookCountDto
            {
                CategoryName = c.Title,
                BooksCount = c.Books.Count(b => b.Stock > 0)
            })
            .ToListAsync();
    }
    
    
}
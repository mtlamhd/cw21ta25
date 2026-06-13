using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.Entities;
using CW21Ta23.Domain.RepositoryInterFaces;
using CW21Ta23.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CW21Ta23.Infrastructure.Repositiries;

public class TagRepository : GenericRepository<Tag>, ITagRepository
{
    public TagRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<TagWithBookCountDto>> GetAllTags()
    {
        return await _context.Tags.AsNoTracking()
            .Select(x => new TagWithBookCountDto
            {
                TagId = x.Id,
                TagName = x.Name,
                BooksCount = x.Books.Count
            }).ToListAsync();
    }

    public async Task<TagWithBookDto?> GetTagWithBookAsync(int tagId)
    {
        return await _context.Tags.AsNoTracking()
            .Where(t => t.Id == tagId)
            .Select(t => new TagWithBookDto
            {
                TagName = t.Name,
                BooksName = t.Books.Select(book => book.Title).ToList()
            }).FirstOrDefaultAsync();
    }

    public async Task<List<TagDto>> GetAllTagsTotal()
    {
        return await _context.Tags.AsNoTracking()
            .Select(t => new TagDto
            {
                Id = t.Id,
                Name = t.Name
            }).ToListAsync();
    }

    public async Task<TagDto?> GetTagById(int id)
    {
        return await _context.Tags.AsNoTracking()
            .Where(t => t.Id == id)
            .Select(t => new TagDto
            {
                Id = t.Id,
                Name = t.Name
            }
            ).FirstOrDefaultAsync();
    }

    public async Task<Tag?> FindWithBooksAsync(int tagId)
    {
        return await _context.Tags
            .Include(t => t.Books)
            .FirstOrDefaultAsync(t  => t.Id == tagId);
    }
}
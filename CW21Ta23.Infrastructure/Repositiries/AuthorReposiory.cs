using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.Entities;
using CW21Ta23.Domain.RepositoryInterFaces;
using CW21Ta23.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CW21Ta23.Infrastructure.Repositiries;

public class AuthorRepository : GenericRepository<Author> , IAuthorRepository
{
    public AuthorRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<AuthorWithBooksCountDto>> GetAuthorsWithBooksCount()
    {
        return await _context.Authors.AsNoTracking()
            .Select(a => new AuthorWithBooksCountDto
            {
                Name = a.FullName,
                Count = a.Books.Count()
            }).ToListAsync();
    }
    
    public async Task<AuthorWithBookDto?> GetAuthorByIdAsync(int authorId)
    {
        return await _context.Authors
            .AsNoTracking()
            .Where(a => a.Id == authorId)
            .Select(a => new AuthorWithBookDto
            {
                AuthorName = a.FullName,
                Country = a.Country,

                Books = a.Books
                    .Select(b => b.Title)
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }
    
    public async Task<List<AuthorWithBooksCountDto>> GetAuthorsWithMoreThanTwoBooksAsync()
    {
        return await _context.Authors
            .AsNoTracking()
            .Where(a => a.Books.Count() > 2)
            .Select(a => new AuthorWithBooksCountDto
            {
                Name = a.FullName, 
                Count = a.Books.Count()
            })
            .ToListAsync();
    }

    public async Task<List<AuthorWithBookDto>> GeAuthorsByName(string authorName)
    {
        return await _context.Authors.AsNoTracking()
            .Where(a => a.FullName.Contains(authorName))
            .Select(a => new AuthorWithBookDto
            {
                AuthorName = a.FullName,
                Country = a.Country,
                Books = a.Books.Select(b => b.Title).ToList()
            }).ToListAsync();
    }
}
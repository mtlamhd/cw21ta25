using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.Entities;

namespace CW21Ta23.Domain.RepositoryInterFaces;

public interface IAuthorRepository : IGenericRepository<Author>
{
    Task<List<AuthorWithBooksCountDto>> GetAuthorsWithBooksCount();
    Task<AuthorWithBookDto?> GetAuthorByIdAsync(int authorId);
    Task<List<AuthorWithBooksCountDto>> GetAuthorsWithMoreThanTwoBooksAsync();
    Task<List<AuthorWithBookDto>> GeAuthorsByName(string authorName);
     Task<Author?> GetByIdWithBookAsync(int authorId);
    
}
using CW21Ta23.Domain.Dto;

namespace CW21Ta23.Domain.ServiceIntefaces;

public interface IAuthorService
{
    Task<List<AuthorWithBooksCountDto>> GetAuthorWithBooksCountAsync();

    Task<List<AuthorTotalPriceDto>> GetAuthorTotalPrices();
    
    Task<List<AuthorWithBooksCountDto>> GetAuthorsWithBooksCount();

    Task<AuthorWithBookDto> GetAuthorByIdAsync(int authorId);

    Task<List<AuthorWithBooksCountDto>> GetAuthorsWithMoreThanTwoBooksAsync();

    Task<List<AuthorWithBookDto>> GeAuthorsByName(string authorName);
}
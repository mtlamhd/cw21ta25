using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.Entities;

namespace CW21Ta23.Domain.RepositoryInterFaces;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<List<BookWithDetailsDto>> GetAllBooksWithDetailsAsync();
    Task<BookWithDetailTotaDto?> GetBookWithDetailsAsync(int bookId);
    Task<Book?> FindWithTagsAsync(int bookId);
    Task<BookWithDetailTotaDto?> GetBookByName(string bookName);
    Task<List<BookModelDto>> GetBookByCategory(int categoryId);
    Task<List<BookModelDto>> GetBookByAuthor(int authorId);
    Task<List<BookModelDto>> GetBookByPublisher(int publisherId);
    Task<List<BookModelDto>> GetBooksByTag(int tagId);
    Task<List<BookModelDto>> GetBooksByPriceRange(decimal minPrice, decimal maxPrice);
    Task<List<BookModelDto>> GetBooksPublishedAfterYear(int year);
}
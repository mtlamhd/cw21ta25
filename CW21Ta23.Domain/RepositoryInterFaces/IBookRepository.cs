using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.Entities;

namespace CW21Ta23.Domain.RepositoryInterFaces;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<List<BookWithDetailsDto>> GetAllBooksWithDetailsAsync();
    Task<BookWithDetailTotaDto?> GetBookWithDetailsAsync(int bookId);
}
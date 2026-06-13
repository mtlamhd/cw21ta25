using CW21Ta23.Domain.Dto;

namespace CW21Ta23.Domain.ServiceIntefaces;

public interface IBookService
{
    Task<bool> BookExists(string title);

    Task<List<BookWithDetailsDto>> GetAllBooks();

    Task<BookWithDetailTotaDto?> GetBookWithDetailsAsync(int bookId);
    Task<List<BookWithStockDto>> GetBooksWithStock();
    Task<BookWithDetailTotaDto?> GetBookByName(string bookName);
    Task<bool> CreateBookAsync(CreateBookDto dto);
}
namespace CW21Ta23.Domain.ServiceIntefaces;

public interface IBookService
{
    Task<bool> BookExists(string title);
}
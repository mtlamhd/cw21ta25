using CW21Ta23.Domain.Dto;

namespace CW21Ta23.Domain.ServiceIntefaces;

public interface ICategoryService
{
    Task<List<CategoryWithBookCountDto>> GetAllCategoriesWithBookCountAsync();
    Task<CategoryDetailDto> GetCategoryByIdAsync(int categoryId);
    Task<List<BookModelDto>> GetBooksByCategoryAsync(int categoryId);
    Task<List<CategoryWithBookCountDto>> GetCategoriesWithAvailableBooksAsync();
}
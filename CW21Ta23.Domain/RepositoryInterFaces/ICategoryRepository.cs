using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.Entities;

namespace CW21Ta23.Domain.RepositoryInterFaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<List<CategoryWithBookCountDto>> GetAllCategoriesWithBookCountAsync();
    Task<CategoryDetailDto?> GetCategoryByIdAsync(int categoryId);
    Task<List<BookModelDto>> GetBooksByCategoryAsync(int categoryId);
    Task<List<CategoryWithBookCountDto>> GetCategoriesWithAvailableBooksAsync();
}
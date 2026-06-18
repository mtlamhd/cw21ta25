using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.Entities;
using CW21Ta23.Domain.RepositoryInterFaces;
using CW21Ta23.Domain.ServiceIntefaces;
using CW21Ta23.Service.Exceptions;

namespace CW21Ta23.Service;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBookRepository _bookRepository;

    public CategoryService(ICategoryRepository categoryRepository, IBookRepository bookRepository)
    {
        _categoryRepository = categoryRepository;
        _bookRepository = bookRepository;
    }

    public async Task<List<CategoryWithDetailDto>> GetCategoriesWithDetailAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var books = await _bookRepository.GetAllAsync();
        return categories.Select(c => new CategoryWithDetailDto
        {
            Name = c.Title,
            BookCount = books.Count(b => b.CategoryId == c.Id),
            AvgPrice = books
                .Where(b => b.CategoryId == c.Id)
                .Select(b => b.Price)
                .DefaultIfEmpty(0)
                .Average()

        }).ToList();
        
    }
        // raveshe 1
    public async Task<List<CategoryReportDto>> GetCategoriesReportAsync()
    {
            var books = await _bookRepository.GetAllAsync();
            var categories = await _categoryRepository.GetAllAsync();

            var grouped = books
                .GroupBy(b => b.CategoryId)
                .Select(g => new CategoryReportDto
                {
                    CategoryName = categories.FirstOrDefault(c => c.Id == g.Key)?.Title ?? "NULLL",
                    BooksCount = g.Count(),
                    TotalStock = g.Sum(b => b.Stock)
                })
                .ToList();

            return grouped;
    }
    
    //raveshe dovom
    
    // public async Task<List<CategoryStockReportDto>> GetCategoryStockReport()
    // {
    //     var categories = await _categoryRepository.GetAllAsync();
    //     var books = await _bookRepository.GetAllAsync();
    //
    //     return categories.Select(c => new CategoryStockReportDto
    //     {
    //         CategoryName = c.Title,
    //         BooksCount = books.Count(b => b.CategoryId == c.Id),
    //         TotalStock = books
    //             .Where(b => b.CategoryId == c.Id)
    //             .Sum(b => b.Stock)
    //     }).ToList();
    // }


    public async Task<List<CategoryWithBookCountDto>> GetAllCategoriesWithBookCountAsync()
    {
        
            return await _categoryRepository.GetAllCategoriesWithBookCountAsync();
    
    }

    public async Task<CategoryDetailDto> GetCategoryByIdAsync(int categoryId)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);

        if (category == null)
            throw new ItemNotFoundException("Category",categoryId);

        return category;
    }

    public async Task<List<BookModelDto>> GetBooksByCategoryAsync(int categoryId)
    {
        var category = await _categoryRepository.FindByIdAsync(categoryId);

        if (category == null)
            throw new ItemNotFoundException("Category",categoryId);

        return await _bookRepository.GetBookByCategory(categoryId);
    }
    
    public async Task<List<CategoryWithBookCountDto>> GetCategoriesWithAvailableBooksAsync()
    {
        return await _categoryRepository.GetCategoriesWithAvailableBooksAsync();
    }

    public async Task<int> CreateCategoryAsync(CreateCategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            throw BadRequestException.Required($"Category Name");

        dto.Title = dto.Title.Trim();

        if (await _categoryRepository.ExistsByTitleAsync(dto.Title))
            throw new ConflictException("Category",$"{dto.Title}");

        var category = new Category
        {
            Title = dto.Title,
            Description = dto.Description
        };

        await _categoryRepository.AddAsync(category);

        return category.Id;
    }

    public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto dto)
    {
        var category = await _categoryRepository.FindByIdAsync(id);

        if (category == null)
            throw new ItemNotFoundException("Category",id);

        if (string.IsNullOrWhiteSpace(dto.Title))
            throw BadRequestException.Required("Category");

        dto.Title = dto.Title.Trim();

        var exists = (await _categoryRepository
                .QueryAsync(c => c.Title == dto.Title && c.Id != id))
            .Any();

        if (exists)
            throw new ConflictException("Category",$"{id}");

        category.Title = dto.Title;
        category.Description = dto.Description;

        await _categoryRepository.UpdateAsync(category);

        return true;
    }
    
    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await _categoryRepository.FindByIdAsync(id);

        if (category == null)
            throw new ItemNotFoundException("Category",id);

        var hasBooks = await _categoryRepository.HasBooksAsync(id);

        if (hasBooks)
            throw BusinessRuleException.CannotDelete("Category", "it has books");

        await _categoryRepository.HardDeleteAsync(category);

        return true;
    }
}
using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.Entities;
using CW21Ta23.Domain.RepositoryInterFaces;
using CW21Ta23.Domain.ServiceIntefaces;

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
        
    
    
    
    }
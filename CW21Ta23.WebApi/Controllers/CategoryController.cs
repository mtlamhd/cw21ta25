using CW21Ta23.Domain.ServiceIntefaces;
using Microsoft.AspNetCore.Mvc;

namespace CW21Ta23.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategoriesWithBookCount()
    {
        var result = await _categoryService.GetAllCategoriesWithBookCountAsync();

        return Ok(result);
    } 
    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetCategoryById(int categoryId)
    {
        try
        {
            var result = await _categoryService.GetCategoryByIdAsync(categoryId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpGet("available")]
    public async Task<IActionResult> GetCategoriesWithAvailableBooks()
    {
        var result = await _categoryService.GetCategoriesWithAvailableBooksAsync();
        return Ok(result);
    }
    
    
    
}
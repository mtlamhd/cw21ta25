using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.ServiceIntefaces;
using CW21Ta23.WebApi.ResultPatterns;
using Microsoft.AspNetCore.Mvc;

namespace CW21Ta23.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _bookService.GetAllBooks();
        
        return Ok(books);
    }

    [HttpGet("{bookId}")]
    public async Task<IActionResult> GetBookById(int bookId)
    {
        try
        {
            var book = await _bookService.GetBookWithDetailsAsync(bookId);
            return Ok(book);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpGet("in-stock")]
    public async Task<IActionResult> GetBooksWithStock()
    {
        try
        {
            var books = await _bookService.GetBooksWithStock();
            return Ok(books);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpGet("search")]
    public async Task<IActionResult> GetBookByName([FromQuery] string bookName)
    {
        try
        {
            var book = await _bookService.GetBookByName(bookName);
            return Ok(book);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookDtoo dto)
    {
        var id = await _bookService.CreateBook(dto);

        var response = GenericResult<int>.Success(id, "Book created successfully", 201);

        return StatusCode(201, response);
    }
    
    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetBookByCategory(int categoryId)
    {
        
        
            var books = await _bookService.GetBookByCategory(categoryId);
            
            var response = GenericResult<List<BookModelDto>>.Success(books, "ok", 200);
                
            return StatusCode(200,response);
    }
    [HttpGet("author/{authorId}")]
    public async Task<IActionResult> GetBookByAuthor(int authorId)
    {
        try
        {
            var books = await _bookService.GetBookByAuthor(authorId);

            return Ok(books);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpGet("publisher/{publisherId}")]
    public async Task<IActionResult> GetBookByPublisher(int publisherId)
    {
        try
        {
            var books = await _bookService.GetBookByPublisher(publisherId);

            return Ok(books);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpGet("tag/{tagId}")]
    public async Task<IActionResult> GetBooksByTag(int tagId)
    {
        try
        {
            var books = await _bookService.GetBooksByTag(tagId);

            return Ok(books);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet("price-range")]
    public async Task<IActionResult> GetBooksByPriceRange(
        decimal minPrice,
        decimal maxPrice)
    {
        try
        {
            var books = await _bookService
                .GetBooksByPriceRange(minPrice, maxPrice);

            return Ok(books);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpGet("published-after")]
    public async Task<IActionResult> GetBooksPublishedAfterYear(int year)
    {
        try
        {
            var books = await _bookService
                .GetBooksPublishedAfterYear(year);

            return Ok(books);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    
}
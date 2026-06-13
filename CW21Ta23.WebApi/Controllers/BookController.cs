using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.ServiceIntefaces;
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
    public async Task<IActionResult> CreateBook([FromBody] CreateBookDto dto)
    {
        try
        {
            await _bookService.CreateBookAsync(dto);
            return Ok("book created");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
}
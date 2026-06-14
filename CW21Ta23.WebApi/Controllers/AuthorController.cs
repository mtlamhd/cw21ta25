using CW21Ta23.Domain.ServiceIntefaces;
using Microsoft.AspNetCore.Mvc;

namespace CW21Ta23.WebApi.Controllers;
[ApiController]
[Route("api/[Controller]")]
public class AuthorController : ControllerBase
{
  private readonly IAuthorService _authorService;

  public AuthorController(IAuthorService authorService)
  {
    _authorService = authorService;
  }
  [HttpGet]
  public async Task<IActionResult>
    GetAllAuthorsWithBookCount()
  {
    var authors = await _authorService
      .GetAuthorsWithBooksCount();

    return Ok(authors);
  }
  
  [HttpGet("{authorId}")]
  public async Task<IActionResult> GetAuthorById(int authorId)
  {
    try
    {
      var author = await _authorService.GetAuthorByIdAsync(authorId);

      return Ok(author);
    }
    catch (Exception ex)
    {
      return NotFound(ex.Message);
    }
  }
  
  [HttpGet("active-authors")]
  public async Task<IActionResult> GetAuthorsWithMoreThanTwoBooks()
  {
    var result = await _authorService.GetAuthorsWithMoreThanTwoBooksAsync();

    return Ok(result);
  }
  
  [HttpGet("search")]
  public async Task<IActionResult> SearchAuthors([FromQuery] string name)
  {
    try
    {
      var result = await _authorService.GeAuthorsByName(name);
      return Ok(result);
    }
    catch (Exception ex)
    {
      return NotFound(ex.Message);
    }
  }
}


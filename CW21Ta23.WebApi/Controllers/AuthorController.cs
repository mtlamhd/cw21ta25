using CW21Ta23.Domain.Dto;
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
  public async Task<IActionResult>GetAllAuthorsWithBookCount()
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
  
  [HttpPost]
  public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto dto)
  {
    try
    {
      var authorId = await _authorService.CreateAuthor(dto);

      return Ok(authorId);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }
  
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteAuthor(int id)
  {
    try
    {
      await _authorService.DeleteAuthorAsync(id);
      return Ok("deleted successfully");
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateAuthor(int id,[FromBody]UpdateAuthorDto dto)
  {
    try
    {
      var updateAuthor = await _authorService.UpdateAuthorAsync(id,dto);
      return Ok(updateAuthor);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
                                                    
    }
  }
}


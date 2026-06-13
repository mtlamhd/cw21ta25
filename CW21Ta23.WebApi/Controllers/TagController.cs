using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.ServiceIntefaces;
using Microsoft.AspNetCore.Mvc;

namespace CW21Ta23.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTags()
    {
        var tags = await _tagService.GetAllTagsTotalAsync();
        return Ok(tags);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTagById(int id)
    {
        var tag = await _tagService.GetTagByIdAsync(id);
        if (tag == null)
        {
            return NotFound();
        }
        return Ok(tag);
    }

    [HttpGet("{id}/books")]
    public async Task<IActionResult> GetBooksByTag(int id)
    {
        var tag = await _tagService.GetBooksByTag(id);
        if(tag==null)
        {
            return NotFound();
        }
        return Ok(tag);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTag([FromBody] CreateTagDto dto)
    {
        var newTag =await _tagService.CreateTag(dto);
        if (newTag == null)
        {
            return NotFound();
        }
        return Ok(newTag);
    }
    
    [HttpPost("add-book")]
    public async Task<IActionResult> AddBookToTag([FromBody] AddTagToBookDto dto)
    {
        try
        {
            await _tagService.AddBookToTagAsync(dto);

            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPost("remove-book")]
    public async Task<IActionResult> RemoveTagFromBook([FromBody] RemoveTagFromBookDto dto)
    {
        try
        {
            var result = await _tagService.RemoveTagFromBookAsync(dto);

            if (!result)
                return NotFound();

            return Ok("book removed from tag successfully");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
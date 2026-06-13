using CW21Ta23.Domain.Dto;

namespace CW21Ta23.Domain.ServiceIntefaces;

public interface ITagService
{
    Task<List<TagDto>> GetAllTagsTotalAsync();
    Task<TagDto?> GetTagByIdAsync(int id);
    Task<TagWithBookDto?> GetBooksByTag(int tagId);
    Task<CreateTagResultDto> CreateTag(CreateTagDto dto);
    Task<bool> AddBookToTagAsync(AddTagToBookDto dto);
    Task<bool> RemoveTagFromBookAsync(RemoveTagFromBookDto dto);
}
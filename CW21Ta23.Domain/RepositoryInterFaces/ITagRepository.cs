using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.Entities;

namespace CW21Ta23.Domain.RepositoryInterFaces;

public interface ITagRepository : IGenericRepository<Tag>
{
    Task<List<TagWithBookCountDto>> GetAllTags();
    Task<TagWithBookDto?> GetTagWithBookAsync(int tagId);
    Task<List<TagDto>> GetAllTagsTotal();
    Task<TagDto?> GetTagById(int id);
    Task<Tag?> FindWithBooksAsync(int tagId);
}
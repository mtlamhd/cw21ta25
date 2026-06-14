using CW21Ta23.Domain.Dto;
using CW21Ta23.Domain.Entities;
using CW21Ta23.Domain.RepositoryInterFaces;
using CW21Ta23.Domain.ServiceIntefaces;
using CW21Ta23.Infrastructure.Repositiries;

namespace CW21Ta23.Service;

public class TagService : ITagService
{
    
    private readonly ITagRepository _tagRepository;
    private readonly IBookRepository _bookRepository;

    public TagService(ITagRepository tagRepository, IBookRepository bookRepository)
    {
        _tagRepository = tagRepository;
        _bookRepository = bookRepository;
    }

    public async Task<List<TagWithBookCountDto>> GetAllTagsWithBookCountAsync()
    {
      return await  _tagRepository.GetAllTags();
    }
    
    public async Task<TagWithBookDto?> GetBooksByTag(int tagId)
    {
        var tag = await _tagRepository.GetTagWithBookAsync(tagId);

        if (tag == null)
            throw new Exception("tag not found");

        return tag;
    }
    public async Task<CreateTagResultDto> CreateTag(CreateTagDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new Exception("tag name is required");

        var existingTags = await _tagRepository
            .QueryAsync(t => t.Name == dto.Name);

        if (existingTags.Any())
            throw new Exception("tag already exists");

        var tag = new Tag
        {
            Name = dto.Name,
            CreatedAt = DateTime.Now
        };

        await _tagRepository.AddAsync(tag);

        return new CreateTagResultDto
        {
            Id = tag.Id,
            Name = tag.Name
        };
    }


    public async Task<List<TagDto>> GetAllTagsTotalAsync()
    {
        return await _tagRepository.GetAllTagsTotal();
    }

    public async Task<TagDto?> GetTagByIdAsync(int id)
    {
        var tag = await _tagRepository.GetTagById(id);
        
        return tag;
    }
    
    public async Task<bool> AddBookToTagAsync(AddTagToBookDto dto)
    {
        var tag = await _tagRepository.FindWithBooksAsync(dto.TagId);

        if (tag == null)
            throw new Exception("tag not found");

        var book = await _bookRepository.FindByIdAsync(dto.BookId);

        if (book == null)
            throw new Exception("book not found");

        if (tag.Books.Any(b => b.Id == dto.BookId))
            throw new Exception("book already added to tag");

        tag.Books.Add(book);

        await _tagRepository.UpdateAsync(tag);

        return true;
    }

    public async Task<bool> RemoveTagFromBookAsync(RemoveTagFromBookDto dto)
    {
        var tag = await _tagRepository.FindWithBooksAsync(dto.TagId);

        if (tag == null)
            throw new Exception("tag not found");

        var book = await _bookRepository.FindByIdAsync(dto.BookId);

        if (book == null)
            throw new Exception("book not found");

        
        var bookInTag = tag.Books.FirstOrDefault(b => b.Id == dto.BookId);

        if (bookInTag == null)
            throw new Exception("book not found in this tag");

        tag.Books.Remove(bookInTag);

        await _tagRepository.UpdateAsync(tag);

        return true;
    }
        
    
    
}
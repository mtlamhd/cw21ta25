using CW21Ta23.Domain.Entities;
using CW21Ta23.Domain.RepositoryInterFaces;
using CW21Ta23.Infrastructure.Data;

namespace CW21Ta23.Infrastructure.Repositiries;

public class AuthorReposiory : GenericRepository<Author> , IAuthorRepository
{
    public AuthorReposiory(AppDbContext context) : base(context)
    {
    }
}
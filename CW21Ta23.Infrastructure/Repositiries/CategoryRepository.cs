using CW21Ta23.Domain.Entities;
using CW21Ta23.Domain.RepositoryInterFaces;
using CW21Ta23.Infrastructure.Data;

namespace CW21Ta23.Infrastructure.Repositiries;

public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}
using System.Linq.Expressions;
using CW21Ta23.Domain.Entities;

namespace CW21Ta23.Domain.RepositoryInterFaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task AddAsync(T entity);
    
    Task SoftDeleteAsync(T entity);
    
    Task UpdateAsync(T entity);
    
    Task HardDeleteAsync(T entity);
    
    Task<T?> FindByIdAsync(int id, bool tracking = false);
    
    Task<List<T>> GetAllAsync(bool tracking = false);
    
    Task<List<T>> QueryAsync(Expression<Func<T, bool>> predicate, bool tracking = false);
    
    Task<bool> ExistsByIdAsync(int id);
    
}
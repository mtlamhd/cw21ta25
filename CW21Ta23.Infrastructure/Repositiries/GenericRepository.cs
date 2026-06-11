using System.Linq.Expressions;
using CW21Ta23.Domain.Entities;
using CW21Ta23.Domain.RepositoryInterFaces;
using CW21Ta23.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CW21Ta23.Infrastructure.Repositiries;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;
    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(T entity)
    {
        entity.IsDeleted = true;
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task HardDeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<T?> FindByIdAsync(int id, bool tracking = false)
    {
        IQueryable<T> query = _dbSet;

        if (!tracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<T>> GetAllAsync(bool tracking = false)
    {
        IQueryable<T> query = _dbSet;

        if (!tracking)
            query = query.AsNoTracking();

        return await query.ToListAsync();
    }

    public async Task<List<T>> QueryAsync(Expression<Func<T, bool>> predicate, bool tracking = false)
    {
        IQueryable<T> query = _dbSet.Where(predicate);

        if (!tracking)
            query = query.AsNoTracking();

        return await query.ToListAsync();
    }
}
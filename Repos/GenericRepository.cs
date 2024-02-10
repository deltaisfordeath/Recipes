using Recipes.Models;
using Microsoft.EntityFrameworkCore;
using Recipes.DAL;


namespace Recipes;

public class GenericRepository<TContext, TEntity>
    where TContext : DbContext
    where TEntity : class
{
    protected readonly TContext _context;

    private DbSet<TEntity> _dbSet;

    protected GenericRepository(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    protected DbSet<TEntity> DbSet
    {
        get
        {
            return _dbSet ??= _context.Set<TEntity>();
        }
    }

    public async Task<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity>> AddAsync(TEntity entity)
    {
        return await DbSet.AddAsync(entity);
    }

    public async Task<TEntity?> FindById(int id)
    {
        return await DbSet.FindAsync(id);
    }
}
using System.Linq.Expressions;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    // TrackChanges to Improve ReadOnly Operations
    private readonly DbContextRepository _dbContext;

    public RepositoryBase(DbContextRepository dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(T entity)
    {
        // _dbContext.Set<T>() => return the DbSet<T> object for the entity type T
        await _dbContext.Set<T>().AddAsync(entity);
    }

    public async Task Delete(T entity)
    {
        // run the task asynchronously ( on a separate thread)
        await Task.Run(() => _dbContext.Set<T>().Remove(entity));
    }

    public async Task Update(T entity)
    {
        await Task.Run(() => _dbContext.Set<T>().Update(entity));
    }

    public async Task<IEnumerable<T>> FindAllAsync(bool trackChanges)
    {
        if (trackChanges)
            return await _dbContext.Set<T>().ToListAsync();
        return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
    }


    // Func<T,bool> is a delegate Type 
    // Expression : 
    // * Represents a strongly typed lambda expression  in the form of an expression tree
    // * The Expression Tree can be translated to sql using Ef core  
    public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression,
        bool trackChanges)
    {
        if (trackChanges)
            return await _dbContext.Set<T>()
                .Where(expression)
                .ToListAsync();
        return await _dbContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
    }
}

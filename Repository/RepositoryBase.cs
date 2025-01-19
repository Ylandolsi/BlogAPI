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
    
    public void Create(T entity)
    {
        // _dbContext.Set<T>() => return the DbSet<T> object for the entity type T
        _dbContext.Set<T>().Add(entity);
    }
    
    public void Delete (T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }
    
    public void Update (T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public IQueryable<T> FindAll(bool trackChanges)
        => trackChanges ? _dbContext.Set<T>() : _dbContext.Set<T>().AsNoTracking(); 
    
    
    // Func<T,bool> is a delegate Type 
    // Expression : 
        // * Represents a strongly typed lambda expression  in the form of an expression tree
        // * The Expression Tree can be translated to sql using Ef core  
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
        bool trackChanges) =>
        !trackChanges ?
            _dbContext.Set<T>()
                .Where(expression)
                .AsNoTracking() :
            _dbContext.Set<T>()
                .Where(expression);
    
    
}
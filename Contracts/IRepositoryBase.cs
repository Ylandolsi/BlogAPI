using System.Collections;
using System.Linq.Expressions;

namespace Contracts;

public interface IRepositoryBase<T> where T : class
{
    Task<IEnumerable<T>> FindAllAsync(bool trackChanges);

    Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression,
        bool trackChanges);

    Task CreateAsync(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}

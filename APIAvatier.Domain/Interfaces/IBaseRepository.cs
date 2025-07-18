using System.Linq.Expressions;

namespace APIAvatier.Domain.Interfaces
{
  public interface IBaseRepository<T> where T : class
  {
    Task<T> Add(T entity);
    Task<T> Edit(T entity);
    Task<int> Delete(int id);
    Task<T?> GetById<TId>(TId id) where TId : notnull;
    Task<IEnumerable<T>> Select(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool trackChanges = false);
    Task<T?> SelectSingle(Expression<Func<T, bool>> predicate);
  }
}
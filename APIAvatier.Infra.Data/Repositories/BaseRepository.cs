using APIAvatier.Domain.Exceptions;
using APIAvatier.Domain.Interfaces;
using APIAvatier.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APIAvatier.Infra.Data.Repositories
{
  public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
  {
    protected readonly DbContext dbContext;
    protected readonly DbSet<T> dbSet;
    protected readonly Context _db;
    public BaseRepository(Context contexto) : base()
    {
      var dbContext = (DbContext)contexto;
      _db = contexto;
      this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
      dbSet = dbContext.Set<T>();
    }
    public virtual async Task<T> Add(T entity)
    {
      try
      {
        await dbSet.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
      }
      catch (Exception)
      {
        throw;
      }
    }
    public virtual async Task<T> Edit(T entity)
    {
      try
      {
        dbContext.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity;
      }
      catch (Exception)
      {
        throw;
      }
    }
    public virtual async Task<int> Delete(int id)
    {
      try
      {
        var entity = await dbSet.FindAsync(id) ?? throw new NotFoundException();
        dbSet.Remove(entity);
        return await dbContext.SaveChangesAsync();
      }
      catch (Exception)
      {
        throw;
      }
    }
    public virtual async Task<T?> GetById<TId>(TId id) where TId : notnull
    {
      try
      {
        return await dbSet.FindAsync(id) ?? throw new NotFoundException();
      }
      catch (Exception)
      {
        throw;
      }
    }
    public virtual async Task<IEnumerable<T>> Select(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool trackChanges = false)
    {
      try
      {
        IQueryable<T> query = dbSet.AsQueryable();
        query = predicate is not null ? query.Where(predicate) : query;
        var ordering = orderBy is not null ? orderBy(query) : query;
        return trackChanges ?
          await ordering.ToListAsync() :
          await ordering.AsNoTracking().ToListAsync();
      }
      catch (Exception)
      {
        throw;
      }
    }
    public virtual async Task<T?> SelectSingle(Expression<Func<T, bool>> predicate)
    {
      try
      {
        IQueryable<T> query = dbSet.AsQueryable();
        return await query.FirstOrDefaultAsync(predicate);
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}
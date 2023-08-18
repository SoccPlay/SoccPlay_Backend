using System.Linq.Expressions;

namespace Application.IRepository.IGeneric;

public interface IGenericRepository<T> where T : class
{
    T GetById(object id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    T SingleOrDefault(Expression<Func<T, bool>> predicate);
    T Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}
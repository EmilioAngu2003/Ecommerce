using System.Linq.Expressions;

namespace Ecommerce.Core.Interfaces;

public interface IRepository<T>
{
    public Task<IReadOnlyList<T>> GetAllAsync();

    public Task<T> GetByIdAsync(int id);

    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
}

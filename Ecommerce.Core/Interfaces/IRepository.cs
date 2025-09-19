namespace Ecommerce.Core.Interfaces;

public interface IRepository<T>
{
    public Task<IReadOnlyList<T>> GetAllAsync();

    public Task<T> GetByIdAsync(int id);
}

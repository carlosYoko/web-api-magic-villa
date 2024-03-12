using System.Linq.Expressions;

namespace MagicVilla_API.IRepository
{
    public interface IRepository<T> where T : class
    {

        Task Create(T entity);

        Task<T?> Get(Expression<Func<T, bool>>? filter = null, bool tracked = true);

        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null);

        Task<T> Remove(T entity);

        Task Save();

    }
}
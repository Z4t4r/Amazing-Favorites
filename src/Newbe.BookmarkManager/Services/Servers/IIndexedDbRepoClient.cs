using System.Collections.Generic;
using System.Threading.Tasks;

namespace Newbe.BookmarkManager.Services.Servers
{
    public interface IIndexedDbRepoClient<T, TKey>
        where T : IEntity<TKey>
    {
        Task<List<T>> GetAllAsync();
        Task UpsertAsync(T entity);
        Task<T?> GetAsync(TKey id);
        Task DeleteAsync(TKey id);
        Task DeleteAllAsync();
    }
}
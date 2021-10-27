using System.Collections.Generic;
using System.Threading.Tasks;

namespace Newbe.BookmarkManager.Services.Servers
{
    public class IndexedDbRepoServer<T, TKey> : IIndexedDbRepoServer<T, TKey> where T : IEntity<TKey>
    {

        private IIndexedDbRepo<T,TKey> _indexedDbRepo;


        public IndexedDbRepoServer(IIndexedDbRepo<T, TKey> indexedDbRepo)
        {
            _indexedDbRepo = indexedDbRepo;
        }

        public async Task<IndexedDbListResponse<T>> GetAllAsync(IndexedDbGetAllAsyncRequest request)
        {
            var list = await _indexedDbRepo.GetAllAsync();
            var result = new IndexedDbListResponse<T>
            {
                Data = list
            };
            return result;
        }
        public async Task<IndexedDbResponse> UpsertAsync(IndexedDbUpsertRequest<T> request)
        {
            await _indexedDbRepo.UpsertAsync(request.Entity);
            return new IndexedDbResponse();
        }

        public async Task<IndexedDbResponse<T>?> GetAsync(IndexedDbGetRequest<TKey> request)
        {
            var entity = await _indexedDbRepo.GetAsync(request.Id);

            var result = new IndexedDbResponse<T>
            {
                Data = entity
            };
            return result;
        }

        public async Task<IndexedDbResponse> DeleteAsync(IndexedDbDeleteRequest<TKey> request)
        {
            await _indexedDbRepo.DeleteAsync(request.Id);
            return new IndexedDbResponse();
        }
        public async Task<IndexedDbResponse> DeleteAllAsync(IndexedDbDeleteAllRequest request)
        {
            await _indexedDbRepo.DeleteAllAsync();
            return new IndexedDbResponse();
        }
    }
    
    public static class IndexedDbRepoServerExtension
    {
        public static async Task<List<T>>  GetAllAsync<T,TKey>(this IndexedDbRepoServer<T,TKey> index) where T : IEntity<TKey>
        {
            return (await index.GetAllAsync(new IndexedDbGetAllAsyncRequest())).Data;
        }

        public static async Task<T> GetAsync<T, TKey>(this IndexedDbRepoServer<T, TKey> index, TKey id) where T : IEntity<TKey>
        {
            return (await index.GetAsync(new IndexedDbGetRequest<TKey>() {Id = id})).Data;
        }
        
        public static async Task UpsertAsync<T, TKey>(this IndexedDbRepoServer<T, TKey> index, T entity) where T : IEntity<TKey>
        {
            await index.UpsertAsync(new IndexedDbUpsertRequest<T>()
            {
                Entity = entity
            });
        }
        
        public static async Task DeleteAsync<T, TKey>(this IndexedDbRepoServer<T, TKey> index, TKey id) where T : IEntity<TKey>
        {
            await index.DeleteAsync(new IndexedDbDeleteRequest<TKey>()
            {
                Id = id
            });
        }
        public static async Task DeleteAllAsync<T, TKey>(this IndexedDbRepoServer<T, TKey> index) where T : IEntity<TKey>
        {
            await index.DeleteAllAsync(new IndexedDbDeleteAllRequest());
        }
        
    }
}
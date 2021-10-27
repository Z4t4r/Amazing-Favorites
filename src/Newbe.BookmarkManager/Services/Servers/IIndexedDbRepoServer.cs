using System.Collections.Generic;
using System.Threading.Tasks;
using Newbe.BookmarkManager.Services.MessageBus;

namespace Newbe.BookmarkManager.Services.Servers
{
    public interface IIndexedDbRepoServer<T, TKey>
        where T : IEntity<TKey>
    {
        Task<IndexedDbListResponse<T>> GetAllAsync(IndexedDbGetAllAsyncRequest request);
        Task<IndexedDbResponse> UpsertAsync(IndexedDbUpsertRequest<T> request);
        Task<IndexedDbResponse<T>?> GetAsync(IndexedDbGetRequest<TKey> request);
        Task<IndexedDbResponse> DeleteAsync(IndexedDbDeleteRequest<TKey> request);
        Task<IndexedDbResponse> DeleteAllAsync(IndexedDbDeleteAllRequest request);
    }
    public record IndexedDbGetAllAsyncRequest:IRequest
    {
        
    }
    
    public record IndexedDbUpsertRequest<T> :IRequest
    {
        public T Entity { get; set; }
    }
    public record IndexedDbGetRequest<TKey> :IRequest
    {
        public TKey Id { get; set; }
    }
    public record IndexedDbDeleteRequest<TKey> :IRequest
    {
        public TKey Id { get; set; }
    }

    public record IndexedDbDeleteAllRequest:IRequest
    {
        
    }
    public record IndexedDbIdRequest<TKey> :IRequest
    {
        public TKey Id { get; set; }
    }
    public record IndexedDbEntityRequest<T> :IRequest
    {
        public T Entity { get; set; }
    }

    public record IndexedDbResponse : IResponse
    {
        
    }
    public record IndexedDbResponse<T> : IResponse
    {
        public T Data { get; set; }
    }
    
    public record IndexedDbListResponse<T> : IResponse
    {
        public List<T> Data { get; set; }
    }
}
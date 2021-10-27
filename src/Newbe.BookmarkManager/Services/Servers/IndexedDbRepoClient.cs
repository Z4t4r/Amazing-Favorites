




using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newbe.BookmarkManager.Services;
using Newbe.BookmarkManager.Services.LPC;
using Newbe.BookmarkManager.Services.Servers;

public class IndexedDbRepoClient<T, TKey>:IIndexedDbRepoClient<T, TKey>
    where T : IEntity<TKey>
{

    private readonly ILPCClient<IIndexedDbRepoServer<T, TKey>> _repoServer;

    private readonly ILogger<IndexedDbRepoClient<T, TKey>> _logger;

    public IndexedDbRepoClient(ILPCClient<IIndexedDbRepoServer<T, TKey>> repoServer,
        ILogger<IndexedDbRepoClient<T, TKey>> logger)
    {
        _repoServer = repoServer;
        _logger = logger;
    }
    
    public async Task<List<T>> GetAllAsync()
    {
        _logger.LogInformation(nameof(T));
        return (await _repoServer.InvokeAsync<IndexedDbGetAllAsyncRequest,IndexedDbListResponse<T>>(new IndexedDbGetAllAsyncRequest())).Data;
    }

    public async Task UpsertAsync(T entity)
    {
        await _repoServer.InvokeAsync<IndexedDbUpsertRequest<T>,IndexedDbResponse>(new IndexedDbUpsertRequest<T>
        {
            Entity = entity
        });
    }

    public async Task<T?> GetAsync(TKey id)
    {
        _logger.LogInformation(nameof(T));
        var entity = (await _repoServer.InvokeAsync<IndexedDbGetRequest<TKey>,IndexedDbResponse<T>>(new IndexedDbGetRequest<TKey>()
        {
            Id = id
        })).Data;
        return entity;
    }

    public async Task DeleteAsync(TKey id)
    {
        await _repoServer.InvokeAsync<IndexedDbDeleteRequest<TKey>,IndexedDbResponse>(new IndexedDbDeleteRequest<TKey>
        {
            Id = id
        });
    }

    public async Task DeleteAllAsync()
    {
        await _repoServer.InvokeAsync<IndexedDbDeleteAllRequest,IndexedDbResponse>(new IndexedDbDeleteAllRequest());
    }
}
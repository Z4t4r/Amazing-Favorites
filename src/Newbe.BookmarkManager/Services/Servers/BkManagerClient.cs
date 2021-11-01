





using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newbe.BookmarkManager.Services;
using Newbe.BookmarkManager.Services.LPC;
using Newbe.BookmarkManager.Services.Servers;
using Newbe.BookmarkManager.WebApi;

public class BkManagerClient : IBkManagerClient
{
    private readonly ILPCClient<IBkManagerServer> _client;

    private readonly ILogger<BkManagerClient> _logger;
    public BkManagerClient(ILPCClient<IBkManagerServer> client, ILogger<BkManagerClient> logger)
    {
        _client = client;
        _logger = logger;
    }
    public async Task AddClickAsync(string url, int moreCount)
    {
        await _client.InvokeAsync<AddClickRequest,BkManagerResponse>(new AddClickRequest
        {
            Url = url,
            MoreCount = moreCount
        });
    }

    public async Task RestoreAsync()
    {
        await _client.InvokeAsync<RestoreRequest,BkManagerResponse>(new RestoreRequest());
    }

    public async Task RemoveTagAsync(string url, string tag)
    {
        await _client.InvokeAsync<RemoveTagRequest,BkManagerResponse>(new RemoveTagRequest
        {
            Url = url,
            Tag = tag
        });
    }

    public async Task<bool> AppendTagAsync(string url, params string[]? tags)
    {
        var result = await _client.InvokeAsync<AppendTagRequest,BkManagerResponse<bool>>(new AppendTagRequest
        {
            Url = url,
            Tags = tags
        });
        return result.Data;
    }

    public async Task UpdateTagsAsync(string url, string title, IEnumerable<string> tags)
    {
        await _client.InvokeAsync<UpdateTagsRequest,BkManagerResponse>(new UpdateTagsRequest
        {
            Url = url,
            Title = title,
            Tags = tags
        });
    }

    public async Task UpdateFavIconUrlAsync(Dictionary<string, string> urls)
    {
        await _client.InvokeAsync<UpdateFavIconUrlRequest,BkManagerResponse>(new UpdateFavIconUrlRequest
        {
            Urls = urls
        });
    }

    public async Task AppendBookmarksAsync(IEnumerable<BookmarkNode> nodes)
    {
        await _client.InvokeAsync<AppendBookmarksRequest,BkManagerResponse>(new AppendBookmarksRequest
        {
            Nodes = nodes
        });
    }

    public async Task LoadCloudCollectionAsync(CloudBkCollection cloudBkCollection)
    {
        await _client.InvokeAsync<LoadCloudCollectionRequest,BkManagerResponse>(new LoadCloudCollectionRequest
        {
            CloudBkCollection = cloudBkCollection
        });
    }

    public async Task<CloudBkCollection> GetCloudBkCollectionAsync()
    {
       var result = await _client.InvokeAsync<GetCloudBkCollectionRequest,BkManagerResponse<CloudBkCollection>>(new GetCloudBkCollectionRequest());

       return result.Data;
    }

    public async Task DeleteAsync(string url)
    {
        await _client.InvokeAsync<DeleteRequest,BkManagerResponse>(new DeleteRequest
        {
            Url = url
        });
    }

    public async Task UpdateTitleAsync(string url, string title)
    {
        await _client.InvokeAsync<UpdateTitleRequest,BkManagerResponse>(new UpdateTitleRequest
        {
            Url = url,
            Title = title
        });
    }

    public async Task<long> GetEtagVersionAsync()
    {
        var result =  await _client.InvokeAsync<GetEtagVersionRequest,BkManagerResponse<long>>(new GetEtagVersionRequest());
        return result.Data;
    }

    public async Task<Bk?> Get(string url)
    {
        var result = await _client.InvokeAsync<GetBkRequest,BkManagerResponse<Bk?>>(new GetBkRequest
        {
            Url = url
        });
        return result.Data;
    }

    public async Task<Dictionary<string, int>> GetTagRelatedCountAsync()
    {
        var result = await _client.InvokeAsync<GetTagRelatedCountRequest,BkManagerResponse<Dictionary<string, int>>>(new GetTagRelatedCountRequest());
        return result.Data;
    }
}
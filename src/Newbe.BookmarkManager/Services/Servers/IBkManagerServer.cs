using System.Collections.Generic;
using System.Threading.Tasks;
using Newbe.BookmarkManager.Services.Ai;
using Newbe.BookmarkManager.Services.MessageBus;
using Newbe.BookmarkManager.WebApi;

namespace Newbe.BookmarkManager.Services.Servers
{
    public interface IBkManagerServer
    {
        Task<BkManagerResponse> AddClickAsync(AddClickRequest request);
        
        Task<BkManagerResponse> RestoreAsync(RestoreRequest request);
        
        Task<BkManagerResponse> RemoveTagAsync(RemoveTagRequest request);
        
        Task<BkManagerResponse> AppendTagAsync(AppendTagRequest request);
        
        Task<BkManagerResponse> UpdateTagsAsync(UpdateTagsRequest request);

        Task<BkManagerResponse> UpdateFavIconUrlAsync(UpdateFavIconUrlRequest request);
        
        Task<BkManagerResponse> AppendBookmarksAsync(AppendBookmarksRequest request);
        
        Task<BkManagerResponse> LoadCloudCollectionAsync(LoadCloudCollectionRequest requst);

        Task<BkManagerResponse<CloudBkCollection>> GetCloudBkCollectionAsync(GetCloudBkCollectionRequest request);
        
        Task<BkManagerResponse> DeleteAsync(DeleteRequest request);
        
        Task<BkManagerResponse> UpdateTitleAsync(UpdateTitleRequest request);
        Task<BkManagerResponse<long>> GetEtagVersionAsync(GetEtagVersionRequest request);
        Task<BkManagerResponse<Bk?>> Get(GetBkRequest request);
        Task<BkManagerResponse<Dictionary<string, int>>> GetTagRelatedCountAsync(GetTagRelatedCountRequest request);
    }

    public record AddClickRequest : IRequest
    {
        public string Url { get; set; }

        public int MoreCount { get; set; }
    }
    
    public record RestoreRequest : IRequest
    {
    }
    
    public record RemoveTagRequest: IRequest
    {
        public string Url { get; set; }

        public string Tag { get; set; }
    }
    public record AppendTagRequest : IRequest
    {
        public string Url { get; set; }

        public string[]? Tags { get; set; }
    }
    
    public record UpdateTagsRequest : IRequest
    {
        public string Url { get; set; }

        public string Title { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
    public record UpdateFavIconUrlRequest: IRequest
    {
        public Dictionary<string, string> Urls { get; set; }
    }
    
   
    public record LoadCloudCollectionRequest: IRequest
    {
        public CloudBkCollection CloudBkCollection { get; set; }
    }
    
    public record AppendBookmarksRequest : IRequest
    {
        public IEnumerable<BookmarkNode> Nodes { get; set; }
    }
    
    public record GetCloudBkCollectionRequest : IRequest
    {
    }
    
    public record DeleteRequest: IRequest
    {
        public string Url { get; set; }
    }
    
    public record UpdateTitleRequest: IRequest
    {
        public string Url { get; set; }

        public string Title { get; set; }
    }
    public record GetEtagVersionRequest : IRequest
    {
    }
    public record GetBkRequest: IRequest
    {
        public string Url { get; set; }
    }
    
    public record GetTagRelatedCountRequest : IRequest
    {
    }   
    

    public record BkManagerResponse : IResponse
    {
        
    }
    public record BkManagerResponse<T> : BkManagerResponse
    {
        public T Data { get; set; }
    }
}
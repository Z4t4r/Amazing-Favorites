using System.Collections.Generic;
using System.Threading.Tasks;
using Newbe.BookmarkManager.WebApi;

namespace Newbe.BookmarkManager.Services.Servers
{
    public class BkManagerServer:IBkManagerServer
    {

        private readonly IBkManager _bkManager;
        public BkManagerServer(IBkManager bkManager)
        {
            _bkManager = bkManager;
        }
        
        public async Task<BkManagerResponse> AddClickAsync(AddClickRequest request)
        {
            await _bkManager.AddClickAsync(request.Url,request.MoreCount);
            return new BkManagerResponse();
        }

        public async Task<BkManagerResponse> RestoreAsync(RestoreRequest request)
        {
            await _bkManager.RestoreAsync();
            return new BkManagerResponse();
        }

        public async Task<BkManagerResponse> RemoveTagAsync(RemoveTagRequest request)
        {
            await _bkManager.RemoveTagAsync(request.Url, request.Tag);
            return new BkManagerResponse();
        }

        public async Task<BkManagerResponse> AppendTagAsync(AppendTagRequest request)
        {
            await _bkManager.AppendTagAsync(request.Url, request.Tags);
            return new BkManagerResponse();
        }

        public async Task<BkManagerResponse> UpdateTagsAsync(UpdateTagsRequest request)
        {
            await _bkManager.UpdateTagsAsync(request.Url,request.Title,request.Tags);
            return new BkManagerResponse();
        }

        public async Task<BkManagerResponse> UpdateFavIconUrlAsync(UpdateFavIconUrlRequest request)
        {
            await _bkManager.UpdateFavIconUrlAsync(request.Urls);
            return new BkManagerResponse();
        }

        public async Task<BkManagerResponse> AppendBookmarksAsync(AppendBookmarksRequest request)
        {
            await _bkManager.AppendBookmarksAsync(request.Nodes);
            return new BkManagerResponse();
        }

        public async Task<BkManagerResponse> LoadCloudCollectionAsync(LoadCloudCollectionRequest requst)
        {
            await _bkManager.LoadCloudCollectionAsync(requst.CloudBkCollection);
            return new BkManagerResponse();
        }

        public async Task<BkManagerResponse<CloudBkCollection>> GetCloudBkCollectionAsync(GetCloudBkCollectionRequest request)
        {
            var result = await _bkManager.GetCloudBkCollectionAsync();
            return new BkManagerResponse<CloudBkCollection>
            {
                Data = result
            };
        }

        public async Task<BkManagerResponse> DeleteAsync(DeleteRequest request)
        {
            await _bkManager.DeleteAsync(request.Url);
            return new BkManagerResponse();
        }

        public async Task<BkManagerResponse> UpdateTitleAsync(UpdateTitleRequest request)
        {
            await _bkManager.UpdateTitleAsync(request.Url,request.Title);
            return new BkManagerResponse();
        }

        public async Task<BkManagerResponse<long>> GetEtagVersionAsync(GetEtagVersionRequest request)
        {
            var result = await _bkManager.GetEtagVersionAsync();
            return new BkManagerResponse<long>
            {
                Data = result
            };
        }

        public async Task<BkManagerResponse<Bk?>> Get(GetBkRequest request)
        {
            var result = await _bkManager.Get(request.Url);
            return new BkManagerResponse<Bk?>
            {
                Data = result
            };
        }

        public async Task<BkManagerResponse<Dictionary<string, int>>> GetTagRelatedCountAsync(GetTagRelatedCountRequest request)
        {
            var result = await _bkManager.GetTagRelatedCountAsync();
            return new BkManagerResponse<Dictionary<string, int>>
            {
                Data = result
            };
        }
    }
}
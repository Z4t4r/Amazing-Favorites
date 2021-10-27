using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newbe.BookmarkManager.Services.LPC;
using Newbe.BookmarkManager.Services.Servers;

namespace Newbe.BookmarkManager.Services
{
    public class LPCServerJob : ILPCServerJob
    {

        private readonly ILPCServer _lpcServer;
        private readonly IBkSearcherServer _bkSearcherServer;
        private readonly IBkSearcher _bkSearcher;

        private readonly IIndexedDbRepoServer<Bk, string> _bkRepoServer;
        private readonly IIndexedDbRepoServer<BkMetadata, string> _bkMetadataRepoServer;
        private readonly IIndexedDbRepoServer<BkTag, string> _tagsRepoServer;
        public LPCServerJob(ILPCServer lpcServer,
            IIndexedDbRepoServer<Bk, string> bkRepoServer,
            IIndexedDbRepoServer<BkMetadata, string> bkMetadataRepoServer,
            IIndexedDbRepoServer<BkTag, string> tagsRepoServer)
        {
            _lpcServer = lpcServer;
            _bkRepoServer = bkRepoServer;
            _bkMetadataRepoServer = bkMetadataRepoServer;
            _tagsRepoServer = tagsRepoServer;
        }

        public async ValueTask StartAsync()
        {
            _lpcServer.AddServerInstance(_bkRepoServer);
            _lpcServer.AddServerInstance(_bkMetadataRepoServer);
            _lpcServer.AddServerInstance(_tagsRepoServer);
            //_lpcServer.AddHandler<BkSearchRequest, BkSearchResponse>(Search);
            await _lpcServer.StartAsync();
        }

        public async Task<BkSearchResponse> Search(BkSearchRequest request)
        {
            var result = await _bkSearcher.Search(request.SearchText, request.Limit);
            var response = new BkSearchResponse
            {
                ResultItems = result
            };
            return response;
        }
    }
}
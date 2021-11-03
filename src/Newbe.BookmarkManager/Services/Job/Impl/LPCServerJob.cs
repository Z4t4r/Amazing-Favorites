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
        private readonly IBkManagerServer _bkManagerServer;
        public LPCServerJob(ILPCServer lpcServer, 
            IBkManagerServer bkManagerServer,
            IBkSearcher bkSearcher,
            IBkSearcherServer bkSearcherServer
            )
        {
            _lpcServer = lpcServer;
            _bkManagerServer = bkManagerServer;
            _bkSearcher = bkSearcher;
            _bkSearcherServer = bkSearcherServer;
        }

        public async ValueTask StartAsync()
        {
             // _lpcServer.AddHandler<BkSearchRequest, BkSearchResponse>(Search);
             // _lpcServer.AddHandler<BkSearchHistoryRequest, BkSearchResponse>(History);
            
            _lpcServer.AddServerInstance(_bkSearcherServer);
             _lpcServer.AddServerInstance(_bkManagerServer);
            await _lpcServer.StartAsync();
        }
        
        
        
    }
}
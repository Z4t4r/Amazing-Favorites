﻿using System.Threading.Tasks;
using Newbe.BookmarkManager.Services.MessageBus;

namespace Newbe.BookmarkManager.Services.Servers
{
    public interface IBkSearcherServer
    {
        Task<BkSearchResponse> SearchAsync(BkSearchRequest request);

        Task<BkSearchResponse> GetHistoryAsync(BkSearchHistoryRequest request);
    }

    public record BkSearchRequest : IRequest
    {
        public string SearchText { get; set; }
        public int Limit { get; set; }
    }

    public record BkSearchHistoryRequest : IRequest
    {
        public int Limit { get; set; }
    }

    public record BkSearchResponse : IResponse
    {
        public SearchResultItem[] ResultItems { get; set; }
    }
}
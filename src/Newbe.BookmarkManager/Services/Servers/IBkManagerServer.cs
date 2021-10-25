using System.Collections.Generic;
using System.Threading.Tasks;
using Newbe.BookmarkManager.Services.Ai;
using Newbe.BookmarkManager.WebApi;
using static Newbe.BookmarkManager.Services.Ai.Events;
namespace Newbe.BookmarkManager.Services.Servers
{
    public interface IBkManagerServer
    {

    }

    public record BkManagerAddRequest
    {
        public string? Url { get; set; }

        public int MoreCount { get; set; }
    }
}
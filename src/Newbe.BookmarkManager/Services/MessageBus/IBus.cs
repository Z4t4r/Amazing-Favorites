using System.Threading.Tasks;
using Newbe.BookmarkManager.Services.LPC;

namespace Newbe.BookmarkManager.Services.MessageBus
{
    public interface IBus
    {
        string BusId { get; }
        Task EnsureStartAsync();
        void RegisterHandler(string messageType, RequestHandlerDelegate handler, string? messageId = null);
        Task SendMessage(BusMessage message);

        Task<IpcResponse> SendRequest(IpcRequest request);


    }
}
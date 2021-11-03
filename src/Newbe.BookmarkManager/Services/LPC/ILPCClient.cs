using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Newbe.BookmarkManager.Services.MessageBus;

namespace Newbe.BookmarkManager.Services.LPC
{
    public interface ILPCClient<out T>
    {
        Task StartAsync();
        Task<TResponse> InvokeAsync<TRequest, TResponse>(TRequest request)
            where TResponse : IResponse
            where TRequest : IRequest;
    }

    public interface IMyLPCClient<TInterface>
        where TInterface : class
    {
        Task InvokeAsync(
            Expression<Action<TInterface>> exp,
            CancellationToken cancellationToken = default);
        Task<TResult> InvokeAsync<TResult>(
            Expression<Func<TInterface, TResult>> exp,
            CancellationToken cancellationToken = default);
    }
}
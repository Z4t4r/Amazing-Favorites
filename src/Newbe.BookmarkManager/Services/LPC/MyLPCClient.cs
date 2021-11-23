

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Newbe.BookmarkManager.Services;
using Newbe.BookmarkManager.Services.LPC;
using Newbe.BookmarkManager.Services.MessageBus;


public abstract class MyLPCClient<TInterface> : IMyLPCClient<TInterface>
    where TInterface : class
{
    
    private readonly IBus _bus;

    public MyLPCClient(
        IBusFactory busFactory)
    {
        _bus = busFactory.Create(new BusOptions
        {
            EnvelopName = Consts.BusEnvelopNames.LPCServer
        });
    }
    
    
    public async Task<TResult> InvokeAsync<TResult>(Expression<Func<TInterface, TResult>> exp,
        CancellationToken cancellationToken = default)
    {
        var request = GetRequest(exp, DispatchProxy.Create<TInterface, IpcProxy<TResult>>());

        var response = await _bus.SendRequest(request);

        return response;
    }
    
    
    private IpcRequest GetRequest(Expression exp, TInterface proxy)
    {
        if (!(exp is LambdaExpression lambdaExp))
        {
            throw new ArgumentException("Only support lambda expression, ex: x => x.GetData(a, b)");
        }

        if (!(lambdaExp.Body is MethodCallExpression methodCallExp))
        {
            throw new ArgumentException("Only support calling method, ex: x => x.GetData(a, b)");
        }

        Delegate @delegate = lambdaExp.Compile();
        @delegate.DynamicInvoke(proxy);
        
        return new IpcRequest
        {
            MethodName = (proxy as IpcProxy).LastInvocation.Method.Name,
            Parameters = (proxy as IpcProxy).LastInvocation.Arguments,

            ParameterTypes = (proxy as IpcProxy).LastInvocation.Method.GetParameters()
                .Select(p => p.ParameterType)
                .ToArray(),


            GenericArguments = (proxy as IpcProxy).LastInvocation.Method.GetGenericArguments(),
        };
    }
    private async Task<TResult> GetResponseAsync<TResult>(IpcRequest request, CancellationToken cancellationToken) where TResult:class
    {
        var result = await _bus.SendRequest<TResult>(request);

        return result;
    }
}
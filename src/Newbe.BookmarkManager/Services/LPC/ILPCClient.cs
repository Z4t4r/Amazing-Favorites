using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
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
        Task<TResult> InvokeAsync<TResult>(
            Expression<Func<TInterface, TResult>> exp,
            CancellationToken cancellationToken = default);
    }
    [DataContract]
    public record IpcRequest
    {
        [DataMember]
        public string MethodName { get; set; }

        [DataMember]
        public IEnumerable<object> Parameters { get; set; }

        [DataMember]
        public IEnumerable<Type> ParameterTypes { get; set; }
        
        [DataMember]
        public IEnumerable<Type> GenericArguments { get; set; }
    }
    
    public class IpcProxy : DispatchProxy
    {
        public Invocation LastInvocation { get; protected set; }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            LastInvocation = new Invocation(targetMethod, args);
            return null;
        }

        public class Invocation
        {
            public Invocation(MethodInfo method, object[] args)
            {
                Method = method;
                Arguments = args;
            }

            public MethodInfo Method { get; }
            public object[] Arguments { get; }
        }
    }

    public class IpcProxy<TResult> : IpcProxy
    {
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            LastInvocation = new Invocation(targetMethod, args);
            return default(TResult);
        }
    }
}
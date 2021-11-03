using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Newbe.BookmarkManager.Services.MessageBus;
using Newtonsoft.Json;

namespace Newbe.BookmarkManager.Services.LPC
{
    public class MyLPCClient<TInterface>
        : IMyLPCClient<TInterface>
        where TInterface : class
    {
        private readonly IBus _bus;

        public MyLPCClient(IBus bus)
        {
            _bus = bus;
        }
        
        public async Task InvokeAsync(Expression<Action<TInterface>> exp, CancellationToken cancellationToken = default)
        {
            var request = GetRequest(exp, DispatchProxy.Create<TInterface, LPCProxy>());

            var response = await _bus.SendRequest2<TInterface>(request);

            //return response;
        }

        public async Task<TResult> InvokeAsync<TResult>(Expression<Func<TInterface, TResult>> exp, CancellationToken cancellationToken = default)
        {
            var request = GetRequest(exp, DispatchProxy.Create<TInterface, LPCProxy>());

            var response = await _bus.SendRequest2<TInterface>(request);
            
            var result = JsonConvert.DeserializeObject<TResult>(JsonConvert.SerializeObject(response));
            
            return result;
        }

        private LPCRequest GetRequest(Expression exp, TInterface proxy)
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

            
            
            return new LPCRequest
            {
                MethodName = (proxy as LPCProxy).LastInvocation.Method.Name,
                Parameters = (proxy as LPCProxy).LastInvocation.Arguments,

                ParameterTypes = (proxy as LPCProxy).LastInvocation.Method.GetParameters()
                              .Select(p => p.ParameterType)
                              .ToArray(),


                GenericArguments = (proxy as LPCProxy).LastInvocation.Method.GetGenericArguments(),
            };
        
        }
    }
    
    
    
    public class LPCProxy : DispatchProxy
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

    public class LPCProxy<TResult> : LPCProxy
    {
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            LastInvocation = new Invocation(targetMethod, args);
            return default(TResult);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newbe.BookmarkManager.Services.MessageBus;

namespace Newbe.BookmarkManager.Services.LPC
{
    public class LPCServer : ILPCServer
    {
        private readonly IBus _bus;
        private readonly ILogger<LPCServer> _logger;

        public LPCServer(
            IBusFactory busFactory, ILogger<LPCServer> logger)
        {
            _logger = logger;
            _bus = busFactory.Create(new BusOptions
            {
                EnvelopName = Consts.BusEnvelopNames.LPCServer
            });
        }

        public ILPCServer AddHandler<TRequest, TResponse>(Func<TRequest, TResponse> handler)
            where TRequest : IRequest
            where TResponse : IResponse
        {
            _bus.RegisterHandler<TRequest>((scope, message, sourceMessage) =>
            {
                var response = handler.Invoke(message);
#pragma warning disable 4014
                _bus.SendResponse(response, sourceMessage);
#pragma warning restore 4014
            });
            return this;
        }

        public ILPCServer AddHandlerAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> handler) where TRequest : IRequest where TResponse : IResponse
        {
            _bus.RegisterHandler<TRequest>((scope, message, sourceMessage) =>
            {
                var response = handler.Invoke(message);
                _bus.SendResponse(response, sourceMessage);
            });
            return this;
        }
        public ILPCServer AddHandlerAsync2<TRequest, TResponse>(Func<TRequest, Task<TResponse>> handler) where TRequest : IRequest where TResponse : IResponse
        {
            _bus.RegisterHandler<TRequest>( (scope, message, sourceMessage) =>
            {
                var response = handler.Invoke(message);
#pragma warning disable 4014
                _bus.SendResponse(response, sourceMessage);
#pragma warning restore 4014
            });
            return this;
        }

        public ILPCServer AddHandler<TRequest, TResponse>(Func<TRequest, Task<TResponse>> handler)
            where TRequest : IRequest
            where TResponse : IResponse
        {
            _bus.RegisterHandler<TRequest>((scope, message, sourceMessage) =>
            {
                var response = handler.Invoke(message);
#pragma warning disable 4014
                _bus.SendResponse(response, sourceMessage);
#pragma warning restore 4014
            });
            return this;
        }

        public async Task StartAsync()
        {
            await _bus.EnsureStartAsync();
            _logger.LogInformation("LPCServer has started");
        }
    }
}
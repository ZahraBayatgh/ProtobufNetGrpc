using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc.Server;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Server.Services;
using Server.Exceptions;

namespace Server.Tests.Config
{
    public class GrpcServiceFixture :  IAsyncDisposable
    {
        public int Port { get; } = PortManager.GetNextPort();
        private readonly Grpc.Core.Server _server;

        public ITestOutputHelper? Output { get; set; }
        public void Log(string message) => Output?.WriteLine(message);

        public GrpcServiceFixture()
        {
            _server = new Grpc.Core.Server
            {
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };

            var counterService = new CounterService();

            _server.Services.AddCodeFirst(counterService, interceptors: new[] { RpcExceptionsInterceptor.Instance });
            _server.Start();
        }
        public async ValueTask DisposeAsync()
        {
            await _server.ShutdownAsync();
        }
    }
}

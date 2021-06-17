using Domain.Models;
using Grpc.Net.Client;
using ProtoBuf;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.Configuration;
using Server.Services;
using Server.Tests.Config;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Server.Tests
{
    public class CounterServiceGrpcTest : IClassFixture<GrpcServiceFixture>, IClassFixture<CustomWebApplicationFactory<Startup>>, IDisposable
    {
        private GrpcClient _grpcClient;
        private ProtoBufMarshallerFactory Marshaller { get; set; }
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly GrpcServiceFixture _fixture;
        private void Log(string message) => _fixture?.Log(message);
        private int Port => _fixture.Port;

        private static readonly ProtoBufMarshallerFactory
            EnableContextualSerializer = (ProtoBufMarshallerFactory)ProtoBufMarshallerFactory.Create(userState: new object()),
            DisableContextualSerializer = (ProtoBufMarshallerFactory)ProtoBufMarshallerFactory.Create(options: ProtoBufMarshallerFactory.Options.DisableContextualSerializer, userState: new object());

        public CounterServiceGrpcTest(CustomWebApplicationFactory<Startup> factory, GrpcServiceFixture fixture, ITestOutputHelper log)
        {
            _fixture = fixture;
            _factory = factory;

            if (fixture != null) fixture.Output = log;
        }

        public void Dispose()
        {
            if (_fixture != null) _fixture.Output = null;
        }

        [Fact]
        public void ProtoBufTest()
        {
            string proto = Serializer.GetProto<DecrementResponse>();
            Assert.True(!string.IsNullOrEmpty(proto));
        }

        [Fact]
        public async Task HttpServerRunningTest()
        {
            // Act - Assert
            HttpClient client = _factory.CreateClient();
            Assert.NotNull(client);

            HttpResponseMessage response = await client.GetAsync("/");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(Startup.HTTP_SERVER_IS_RUNNING_MESSAGE, responseString);
        }

        [Theory]
        [InlineData(true)]
        public async Task CounterService_Increment_Grpc_Test(bool disableContextual)
        {
            // Arrange
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            using var http = GrpcChannel.ForAddress($"http://localhost:{Port}");

            Marshaller = disableContextual ? DisableContextualSerializer : EnableContextualSerializer;
            _grpcClient = new GrpcClient(http, nameof(CounterService), BinderConfiguration.Create(new MarshallerFactory[] { Marshaller }));

            IncrementRequest incrementRequest = new IncrementRequest { Inc=2 };

            // Act
            IncrementResponce response = await _grpcClient.UnaryAsync<IncrementRequest, IncrementResponce>(incrementRequest, nameof(CounterService.Increment));
            var expected = new IncrementResponce { Result=2};

            // Assert
            Assert.Equal(response.Result, expected.Result);
        }


    }
}

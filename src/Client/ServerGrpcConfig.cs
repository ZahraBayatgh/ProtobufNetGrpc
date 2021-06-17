using Domain.IServices;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;

namespace Client
{
    public class ServerGrpcConfig
    {
        private readonly GrpcChannel _channel;

        public ServerGrpcConfig(string arzshomarOnUrl)
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            _channel = GrpcChannel.ForAddress(arzshomarOnUrl);

        }
        public ICounterService CreateCounterServiceGrpc()
        {
            return _channel.CreateGrpcService<ICounterService>();
        }
    }
}

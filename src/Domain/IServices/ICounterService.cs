using Domain.Models;
using ProtoBuf.Grpc.Configuration;
using System.ServiceModel;

namespace Domain.IServices
{
    [ServiceContract(Name = "CounterService")]
    public interface ICounterService
    {
        [SimpleRpcExceptions]
        IncrementResponce Increment(IncrementRequest request);

        [SimpleRpcExceptions]
        DecrementResponse Decrement(DecrementRequest request);
    }
}


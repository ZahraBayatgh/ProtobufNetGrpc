using Grpc.Core;
using ProtoBuf.Grpc.Configuration;
using System;

namespace Server.Exceptions
{
    public class RpcExceptionsInterceptor : ServerExceptionsInterceptorBase
    {
        private RpcExceptionsInterceptor() { }
        private static RpcExceptionsInterceptor? _sInstance;

        public static RpcExceptionsInterceptor Instance => _sInstance ??= new RpcExceptionsInterceptor();

        private static bool ShouldWrap(Exception exception, out Status status)
        {
            status = new Status(
                    StatusCode.Internal
                    , exception.Message, exception);

            return true;
        }

        protected override bool OnException(Exception exception, out Status status)
            => ShouldWrap(exception, out status);

    }
}

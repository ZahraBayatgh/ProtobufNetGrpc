using Domain.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using ProtoBuf.Grpc.Server;
using Server.Exceptions;
using Server.Services;
using System.IO.Compression;

namespace Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCodeFirstGrpc(config =>
            {
                config.Interceptors.Add(typeof(RpcExceptionsInterceptor));
                config.ResponseCompressionLevel = CompressionLevel.Optimal;
            });

            services.TryAddSingleton(RpcExceptionsInterceptor.Instance);
            services.AddCodeFirstGrpcReflection();

            services.AddScoped<ICounterService, CounterService>();

        }
        public const string HTTP_SERVER_IS_RUNNING_MESSAGE = "Http server is running";
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<ICounterService>();
                endpoints.MapCodeFirstGrpcReflectionService();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(HTTP_SERVER_IS_RUNNING_MESSAGE);
                });
            });
        }
    }
}


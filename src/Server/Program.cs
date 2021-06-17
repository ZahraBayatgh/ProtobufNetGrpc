using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.ConfigureKestrel(options =>
                     {
                         // Set properties and call methods on options
                         options.Limits.Http2.MaxStreamsPerConnection = 100;
                         options.Limits.Http2.HeaderTableSize = 4096;
                         options.ListenLocalhost(14001);
                         options.ConfigureEndpointDefaults(p => p.Protocols = HttpProtocols.Http2);

                     }).UseStartup<Startup>();
                 });
    }
}


using System.IO;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ReleaseWebServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var config = new ConfigurationBuilder()
            .AddCommandLine(args)
            .AddEnvironmentVariables(prefix: "ASPNETCORE_")
            .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel(/*options => options.Listen(IPAddress.Any, 80)*/)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

        //    public static IHostBuilder CreateHostBuilder(string[] args) =>

        //        new WebHostBuilder().UseConfiguration()
        //        //Host.CreateDefaultBuilder(args).
        //        //    .ConfigureWebHostDefaults(webBuilder =>
        //        //    {
        //        //        webBuilder.UseStartup<Startup>();
        //        //    });


        //}
    }
}
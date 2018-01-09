using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SalesApi.Web.Configurations;
using Serilog;

namespace SalesApi.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "MLH Sales";
            SerilogConfigure.ConfigureSerilog();
            try
            {
                Log.Information("Starting MLH Sales web host");
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseIISIntegration()
                .UseUrls(Environment.GetEnvironmentVariable("MLH:SalesApi:ServerBase"))
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
    }
}

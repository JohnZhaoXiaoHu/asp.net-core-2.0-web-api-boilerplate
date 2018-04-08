using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Sales.Api.Configurations;
using Serilog;

namespace Sales.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Sales API";
            SerilogConfigure.ConfigureSerilog();
            try
            {
                Log.Information("Starting Sales API web host");
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
                .UseUrls(Environment.GetEnvironmentVariable("Dave:SalesApi:ServerBase"))
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
    }
}

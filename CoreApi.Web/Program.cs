using System;
using CoreApi.DataContext.Core;
using CoreApi.DataContext.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CoreApi.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var host = BuildWebHost(args);
                try
                {
                    using (var scope = host.Services.CreateScope())
                    {
                        var services = scope.ServiceProvider;
                        var context = services.GetRequiredService<CoreContext>();
                        context.EnsureSeedDataForContext();
                    }
                }
                catch (Exception ex)
                {
                    var logger = host.Services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
                host.Run();
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
                .UseUrls("http://0.0.0.0:5001")
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
    }
}

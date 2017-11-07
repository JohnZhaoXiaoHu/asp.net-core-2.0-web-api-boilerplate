using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AuthorizationServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Authorization Server";

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://0.0.0.0:5000")
                .UseStartup<Startup>()
                .Build();
    }
}

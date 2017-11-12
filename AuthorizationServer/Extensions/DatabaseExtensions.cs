using System.Linq;
using System.Threading.Tasks;
using AuthorizationServer.Configuration;
using AuthorizationServer.Data;
using AuthorizationServer.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorizationServer.Extensions
{
    public static class DatabaseExtensions
    {
        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var persistedGrantDbContext = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
                var configurationDbContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                
                applicationDbContext.Database.Migrate();
                persistedGrantDbContext.Database.Migrate();
                configurationDbContext.Database.Migrate();

                if (!configurationDbContext.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        configurationDbContext.Clients.Add(client.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }

                if (!configurationDbContext.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        configurationDbContext.IdentityResources.Add(resource.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }

                if (!configurationDbContext.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        configurationDbContext.ApiResources.Add(resource.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                Task.Run(async () =>
                {
                    var admin = await userManager.FindByNameAsync("admin");
                    if (admin == null)
                    {
                        await userManager.CreateAsync(new ApplicationUser
                        {
                            UserName = "admin",
                            Email = "emailyx@gmail.com"
                        }, "admin");
                    }
                }).GetAwaiter().GetResult();
                
            }
        }
    }
}

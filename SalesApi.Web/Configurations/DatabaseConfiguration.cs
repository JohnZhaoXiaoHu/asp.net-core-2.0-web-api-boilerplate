using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesApi.Core.Contexts;

namespace SalesApi.Web.Configurations
{
    public static class DatabaseExtensions
    {
        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var salesContext = serviceScope.ServiceProvider.GetRequiredService<SalesContext>();

                salesContext.Database.Migrate();
            }
        }
    }
}

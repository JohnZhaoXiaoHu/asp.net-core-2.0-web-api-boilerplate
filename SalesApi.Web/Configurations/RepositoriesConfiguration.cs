using Microsoft.Extensions.DependencyInjection;
using SalesApi.Infrastructure.IRepositories.Settings;
using SalesApi.Repositories.Settings;

namespace SalesApi.Web.Configurations
{
    public static class RepositoriesConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using SalesApi.Core.IServices;
using SalesApi.Services;

namespace SalesApi.Web.Configurations
{
    public static class ServicesConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            // services.AddScoped(typeof(ICollectiveService<>), typeof(CollectiveService<>));
        }
    }
}

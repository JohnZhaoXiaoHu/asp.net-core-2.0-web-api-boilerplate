using Microsoft.Extensions.DependencyInjection;
using SalesApi.Core.IServices.Settings;
using SalesApi.Services.Settings;

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

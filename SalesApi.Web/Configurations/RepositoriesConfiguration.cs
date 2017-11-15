using Microsoft.Extensions.DependencyInjection;
using SalesApi.Repositories.Settings;

namespace SalesApi.Web.Configurations
{
    public static class RepositoriesConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IDistributionGroupRepository, DistributionGroupRepository>();
            services.AddScoped<IDeliveryVehicleRepository, DeliveryVehicleRepository>();
        }
    }
}

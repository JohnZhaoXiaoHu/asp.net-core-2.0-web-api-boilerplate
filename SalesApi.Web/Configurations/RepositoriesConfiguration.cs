using Microsoft.Extensions.DependencyInjection;
using SalesApi.Repositories.Retail;
using SalesApi.Repositories.Settings;

namespace SalesApi.Web.Configurations
{
    public static class RepositoriesConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            #region Settings

            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IDistributionGroupRepository, DistributionGroupRepository>();
            services.AddScoped<IDeliveryVehicleRepository, DeliveryVehicleRepository>();
            services.AddScoped<ISubAreaRepository, SubAreaRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            #endregion

            #region Retail

            services.AddScoped<IProductForRetailRepository, ProductForRetailRepository>();
            services.AddScoped<IRetailerRepository, RetailerRepository>();

            #endregion
        }
    }
}

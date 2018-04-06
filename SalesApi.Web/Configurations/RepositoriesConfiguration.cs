using Microsoft.Extensions.DependencyInjection;
using SalesApi.Core.IRepositories;
using SalesApi.Repositories;

namespace SalesApi.Web.Configurations
{
    public static class RepositoriesConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}

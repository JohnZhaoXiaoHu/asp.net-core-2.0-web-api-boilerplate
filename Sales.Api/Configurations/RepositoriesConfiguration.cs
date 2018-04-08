using Microsoft.Extensions.DependencyInjection;
using Sales.Core.Interfaces;
using Sales.Infrastructure.Data;
using Sales.Infrastructure.Repositories;

namespace Sales.Api.Configurations
{
    public static class RepositoriesConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        }
    }
}

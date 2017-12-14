using Microsoft.Extensions.DependencyInjection;
using SalesApi.Repositories.Collective;
using SalesApi.Repositories.Overall;
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

            #region Overall

            services.AddScoped<ISalesDayRepository, SalesDayRepository>();

            #endregion

            #region Retail

            services.AddScoped<IProductForRetailRepository, ProductForRetailRepository>();
            services.AddScoped<IRetailerRepository, RetailerRepository>();
            services.AddScoped<IRetailPromotionSeriesRepository, RetailPromotionSeriesRepository>();
            services.AddScoped<IRetailPromotionSeriesBonusRepository, RetailPromotionSeriesBonusRepository>();
            services.AddScoped<IRetailPromotionEventRepository, RetailPromotionEventRepository>();
            services.AddScoped<IRetailPromotionEventBonusRepository, RetailPromotionEventBonusRepository>();
            services.AddScoped<IRetailDayRepository, RetailDayRepository>();
            services.AddScoped<IRetailProductSnapshotRepository, RetailProductSnapshotRepository>();
            services.AddScoped<IRetailOrderRepository, RetailOrderRepository>();

            #endregion

            #region Collective

            services.AddScoped<IProductForCollectiveRepository, ProductForCollectiveRepository>();
            services.AddScoped<ICollectiveCustomerRepository, CollectiveCustomerRepository>();
            services.AddScoped<ICollectiveDayRepository, CollectiveDayRepository>();
            services.AddScoped<ICollectiveProductSnapshotRepository, CollectiveProductSnapshotRepository>();
            services.AddScoped<ICollectiveOrderRepository, CollectiveOrderRepository>();

            #endregion
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using SalesApi.Repositories.Collective;
using SalesApi.Repositories.County;
using SalesApi.Repositories.Mall;
using SalesApi.Repositories.Overall;
using SalesApi.Repositories.Retail;
using SalesApi.Repositories.Settings;
using SalesApi.Repositories.Subscription;
using SalesApi.Repositories.Subscription.Order;
using SalesApi.Repositories.Subscription.Promotion;

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
            services.AddScoped<IRetailPromotionGiftOrderRepository, RetailPromotionGiftOrderRepository>();

            #endregion

            #region Collective

            services.AddScoped<IProductForCollectiveRepository, ProductForCollectiveRepository>();
            services.AddScoped<ICollectiveCustomerRepository, CollectiveCustomerRepository>();
            services.AddScoped<ICollectiveDayRepository, CollectiveDayRepository>();
            services.AddScoped<ICollectiveProductSnapshotRepository, CollectiveProductSnapshotRepository>();
            services.AddScoped<ICollectiveOrderRepository, CollectiveOrderRepository>();
            services.AddScoped<ICollectivePriceRepository, CollectivePriceRepository>();

            #endregion

            #region County

            services.AddScoped<IProductForCountyRepository, ProductForCountyRepository>();
            services.AddScoped<ICountyAgentRepository, CountyAgentRepository>();
            services.AddScoped<ICountyAgentPriceRepository, CountyAgentPriceRepository>();
            services.AddScoped<ICountyDayRepository, CountyDayRepository>();
            services.AddScoped<ICountyProductSnapshotRepository, CountyProductSnapshotRepository>();
            services.AddScoped<ICountyOrderRepository, CountyOrderRepository>();
            services.AddScoped<ICountyPromotionSeriesRepository, CountyPromotionSeriesRepository>();
            services.AddScoped<ICountyPromotionSeriesBonusRepository, CountyPromotionSeriesBonusRepository>();
            services.AddScoped<ICountyPromotionEventRepository, CountyPromotionEventRepository>();
            services.AddScoped<ICountyPromotionEventBonusRepository, CountyPromotionEventBonusRepository>();
            services.AddScoped<ICountyPromotionGiftOrderRepository, CountyPromotionGiftOrderRepository>();

            #endregion

            #region Mall

            services.AddScoped<IProductForMallRepository, ProductForMallRepository>();
            services.AddScoped<IMallGroupRepository, MallGroupRepository>();
            services.AddScoped<IMallCustomerRepository, MallCustomerRepository>();
            services.AddScoped<IMallDayRepository, MallDayRepository>();
            services.AddScoped<IMallProductSnapshotRepository, MallProductSnapshotRepository>();
            services.AddScoped<IMallOrderRepository, MallOrderRepository>();
            services.AddScoped<IMallPriceRepository, MallPriceRepository>();

            #endregion

            #region Subscription

            services.AddScoped<IProductForSubscriptionRepository, ProductForSubscriptionRepository>();
            services.AddScoped<ISubscriptionDayRepository, SubscriptionDayRepository>();
            services.AddScoped<ISubscriptionProductSnapshotRepository, SubscriptionProductSnapshotRepository>();
            services.AddScoped<IMilkmanRepository, MilkmanRepository>();
            services.AddScoped<ISubscriptionMonthPromotionRepository, SubscriptionMonthPromotionRepository>();
            services.AddScoped<ISubscriptionMonthPromotionBonusRepository, SubscriptionMonthPromotionBonusRepository>();
            services.AddScoped<ISubscriptionMonthPromotionBonusDateRepository, SubscriptionMonthPromotionBonusDateRepository>();
            services.AddScoped<ISubscriptionOrderRepository, SubscriptionOrderRepository>();
            services.AddScoped<ISubscriptionOrderDateRepository, SubscriptionOrderDateRepository>();
            services.AddScoped<ISubscriptionOrderBonusDateRepository, SubscriptionOrderBonusDateRepository>();

            #endregion

        }
    }
}

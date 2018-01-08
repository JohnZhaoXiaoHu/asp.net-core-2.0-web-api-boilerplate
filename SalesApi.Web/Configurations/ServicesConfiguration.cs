using Microsoft.Extensions.DependencyInjection;
using SalesApi.Services.Collective;
using SalesApi.Services.County;
using SalesApi.Services.Mall;
using SalesApi.Services.Retail;
using SalesApi.Services.Subscription;

namespace SalesApi.Web.Configurations
{
    public static class ServicesConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            #region Settings

            #endregion

            #region Overall

            #endregion

            #region Retail

            services.AddScoped(typeof(IRetailService<>), typeof(RetailService<>));
            services.AddScoped<IRetailDayService, RetailDayService>();
            services.AddScoped<IRetailOrderService, RetailOrderService>();

            #endregion

            #region Collective

            services.AddScoped(typeof(ICollectiveService<>), typeof(CollectiveService<>));
            services.AddScoped<ICollectiveDayService, CollectiveDayService>();

            #endregion

            #region County

            services.AddScoped(typeof(ICountyService<>), typeof(CountyService<>));
            services.AddScoped<ICountyDayService, CountyDayService>();
            services.AddScoped<ICountyOrderService, CountyOrderService>();

            #endregion

            #region Mall

            services.AddScoped(typeof(IMallService<>), typeof(MallService<>));
            services.AddScoped<IMallDayService, MallDayService>();

            #endregion

            #region Subscription

            services.AddScoped(typeof(ISubscriptionService<>), typeof(SubscriptionService<>));
            services.AddScoped<ISubscriptionDayService, SubscriptionDayService>();

            #endregion
        }
    }
}

using AutoMapper;
using SalesApi.Models.Collective;
using SalesApi.Models.County;
using SalesApi.Models.Mall;
using SalesApi.Models.Overall;
using SalesApi.Models.Retail;
using SalesApi.Models.Settings;
using SalesApi.Models.Subscription;
using SalesApi.Models.Subscription.Promotion;
using SalesApi.ViewModels.Collective;
using SalesApi.ViewModels.County;
using SalesApi.ViewModels.Mall;
using SalesApi.ViewModels.Overall;
using SalesApi.ViewModels.Retail;
using SalesApi.ViewModels.Settings;
using SalesApi.ViewModels.Subscription;
using SalesApi.ViewModels.Subscription.Promotion;

namespace SalesApi.Web.Configurations
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => "ViewModelToDomainMappings";

        public ViewModelToDomainMappingProfile()
        {
            #region Settings

            CreateMap<VehicleViewModel, Vehicle>();
            CreateMap<DistributionGroupViewModel, DistributionGroup>();
            CreateMap<DeliveryVehicleViewModel, DeliveryVehicle>();
            CreateMap<SubAreaAddViewModel, SubArea>();
            CreateMap<SubAreaEditViewModel, SubArea>();
            CreateMap<ProductViewModel, Product>();

            #endregion

            #region Overall

            CreateMap<SalesDayViewModel, SalesDay>();

            #endregion

            #region Retail

            CreateMap<ProductForRetailViewModel, ProductForRetail>();
            CreateMap<RetailerViewModel, Retailer>();
            CreateMap<RetailPromotionSeriesBonusViewModel, RetailPromotionSeriesBonus>();
            CreateMap<RetailPromotionEventViewModel, RetailPromotionEvent>();
            CreateMap<RetailPromotionEventBonusViewModel, RetailPromotionEventBonus>();
            CreateMap<RetailPromotionSeriesAddViewModel, RetailPromotionSeries>();
            CreateMap<RetailPromotionSeriesEditViewModel, RetailPromotionSeries>();
            CreateMap<RetailDayViewModel, RetailDay>();
            CreateMap<RetailProductSnapshotViewModel, RetailProductSnapshot>();
            CreateMap<RetailOrderViewModel, RetailOrder>();
            CreateMap<RetailPromotionGiftOrderViewModel, RetailPromotionGiftOrder>();

            #endregion

            #region Collective

            CreateMap<ProductForCollectiveViewModel, ProductForCollective>();
            CreateMap<CollectiveCustomerViewModel, CollectiveCustomer>();
            CreateMap<CollectiveDayViewModel, CollectiveDay>();
            CreateMap<CollectiveProductSnapshotViewModel, CollectiveProductSnapshot>();
            CreateMap<CollectiveOrderViewModel, CollectiveOrder>();
            CreateMap<CollectivePriceViewModel, CollectivePrice>();
            CreateMap<CollectiveOrderSetPriceViewModel, CollectiveOrder>();

            #endregion

            #region County

            CreateMap<ProductForCountyViewModel, ProductForCounty>();
            CreateMap<CountyAgentViewModel, CountyAgent>();
            CreateMap<CountyAgentPriceViewModel, CountyAgentPrice>();
            CreateMap<CountyDayViewModel, CountyDay>();
            CreateMap<CountyProductSnapshotViewModel, CountyProductSnapshot>();
            CreateMap<CountyOrderViewModel, CountyOrder>();
            CreateMap<CountyOrderSetPriceViewModel, CountyOrder>();
            CreateMap<CountyPromotionSeriesBonusViewModel, CountyPromotionSeriesBonus>();
            CreateMap<CountyPromotionEventViewModel, CountyPromotionEvent>();
            CreateMap<CountyPromotionEventBonusViewModel, CountyPromotionEventBonus>();
            CreateMap<CountyPromotionSeriesAddViewModel, CountyPromotionSeries>();
            CreateMap<CountyPromotionSeriesEditViewModel, CountyPromotionSeries>();
            CreateMap<CountyPromotionGiftOrderViewModel, CountyPromotionGiftOrder>();

            #endregion

            #region Mall

            CreateMap<ProductForMallViewModel, ProductForMall>();
            CreateMap<MallGroupViewModel, MallGroup>();
            CreateMap<MallCustomerViewModel, MallCustomer>();
            CreateMap<MallDayViewModel, MallDay>();
            CreateMap<MallProductSnapshotViewModel, MallProductSnapshot>();
            CreateMap<MallOrderViewModel, MallOrder>();
            CreateMap<MallPriceViewModel, MallPrice>();
            CreateMap<MallOrderSetPriceViewModel, MallOrder>();

            #endregion

            #region Subscription

            CreateMap<ProductForSubscriptionViewModel, ProductForSubscription>();
            CreateMap<SubscriptionDayViewModel, SubscriptionDay>();
            CreateMap<SubscriptionProductSnapshotViewModel, SubscriptionProductSnapshot>();
            CreateMap<MilkmanViewModel, Milkman>();
            CreateMap<SubscriptionMonthPromotionViewModel, SubscriptionMonthPromotion>();
            CreateMap<SubscriptionMonthPromotionBonusViewModel, SubscriptionMonthPromotionBonus>();
            CreateMap<SubscriptionMonthPromotionBonusDateViewModel, SubscriptionMonthPromotionBonusDate>();

            #endregion

        }
    }
}
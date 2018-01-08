using AutoMapper;
using SalesApi.Models.Collective;
using SalesApi.Models.County;
using SalesApi.Models.Mall;
using SalesApi.Models.Overall;
using SalesApi.Models.Retail;
using SalesApi.Models.Settings;
using SalesApi.Models.Subscription;
using SalesApi.ViewModels.Collective;
using SalesApi.ViewModels.County;
using SalesApi.ViewModels.Mall;
using SalesApi.ViewModels.Overall;
using SalesApi.ViewModels.Retail;
using SalesApi.ViewModels.Settings;
using SalesApi.ViewModels.Subscription;

namespace SalesApi.Web.Configurations
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToViewModelMappings";

        public DomainToViewModelMappingProfile()
        {
            #region Settings

            CreateMap<Vehicle, VehicleViewModel>();
            CreateMap<DistributionGroup, DistributionGroupViewModel>();
            CreateMap<DeliveryVehicle, DeliveryVehicleViewModel>()
                .ForMember(dest => dest.VehicleName, opt => opt.MapFrom(src => src.Vehicle.Name));
            CreateMap<SubArea, SubAreaViewModel>()
                .ForMember(dest => dest.SalesType, opt => opt.MapFrom(src => src.DeliveryVehicle.SalesType))
                .ForMember(dest => dest.SalesTypeName, opt => opt.MapFrom(src => src.DeliveryVehicle.SalesType.ToString()))
                .ForMember(dest => dest.VehicleName, opt => opt.MapFrom(src => src.DeliveryVehicle.Vehicle.Name));
            CreateMap<SubArea, SubAreaEditViewModel>();
            CreateMap<Product, ProductViewModel>();

            #endregion

            #region Overall

            CreateMap<SalesDay, SalesDayViewModel>();

            #endregion

            #region Retail

            CreateMap<ProductForRetail, ProductForRetailViewModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product == null ? null : src.Product.Name))
                .ForMember(d => d.Order, o => o.MapFrom(s => s.Product != null ? s.Product.Order : s.Order))
                .ForMember(d => d.ProductPinyin, o => o.MapFrom(s => s.Product != null ? s.Product.Pinyin : null));
            CreateMap<Retailer, RetailerViewModel>();
            CreateMap<RetailPromotionSeries, RetailPromotionSeriesViewModel>();
            CreateMap<RetailPromotionSeriesBonus, RetailPromotionSeriesBonusViewModel>();
            CreateMap<RetailPromotionEvent, RetailPromotionEventViewModel>()
                .ForMember(d => d.SeriesName, o => o.MapFrom(s => s.RetailPromotionSeries == null ? null : s.RetailPromotionSeries.Name));
            CreateMap<RetailPromotionEvent, RetailPromotionEventForFullCalendarViewModel>()
                .ForMember(d => d.Title, o => o.MapFrom(s => $"[{s.RetailPromotionSeriesId}] {s.Name}"))
                .ForMember(d => d.Start, o => o.MapFrom(s => s.Date))
                .ForMember(d => d.End, o => o.MapFrom(s => s.Date))
                .ForMember(d => d.AllDay, o => o.MapFrom(s => true))
                .ForMember(d => d.Editable, o => o.MapFrom(s => false));
            CreateMap<RetailPromotionEventBonus, RetailPromotionEventBonusViewModel>();
            CreateMap<RetailDay, RetailDayViewModel>();
            CreateMap<RetailProductSnapshot, RetailProductSnapshotViewModel>();
            CreateMap<RetailOrder, RetailOrderViewModel>();
            CreateMap<RetailPromotionGiftOrder, RetailPromotionGiftOrderViewModel>()
                .ForMember(d => d.ProductForRetailId, o => o.MapFrom(s => s.RetailPromotionEventBonus.ProductForRetailId))
                .ForMember(d => d.RetailPromotionEventId, o => o.MapFrom(s => s.RetailPromotionEventBonus.RetailPromotionEventId));
            CreateMap<RetailOrder, RetailOrderWithGiftViewModel>();

            #endregion

            #region Collective

            CreateMap<ProductForCollective, ProductForCollectiveViewModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product == null ? null : src.Product.Name))
                .ForMember(d => d.Order, o => o.MapFrom(s => s.Product != null ? s.Product.Order : s.Order))
                .ForMember(d => d.ProductPinyin, o => o.MapFrom(s => s.Product != null ? s.Product.Pinyin : null));
            CreateMap<CollectiveCustomer, CollectiveCustomerViewModel>();
            CreateMap<CollectiveDay, CollectiveDayViewModel>();
            CreateMap<CollectiveProductSnapshot, CollectiveProductSnapshotViewModel>();
            CreateMap<CollectiveOrder, CollectiveOrderViewModel>()
                .ForMember(d => d.ProductForCollectiveId, o => o.MapFrom(s => s.CollectiveProductSnapshot != null ? (int?)s.CollectiveProductSnapshot.ProductForCollectiveId : null));
            CreateMap<CollectivePrice, CollectivePriceViewModel>();
            CreateMap<CollectiveOrder, CollectiveOrderSetPriceViewModel>()
                .ForMember(d => d.ProductForCollectiveId,
                    o => o.MapFrom(s => s.CollectiveProductSnapshot.ProductForCollectiveId));

            #endregion

            #region County

            CreateMap<ProductForCounty, ProductForCountyViewModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product == null ? null : src.Product.Name))
                .ForMember(d => d.Order, o => o.MapFrom(s => s.Product != null ? s.Product.Order : s.Order))
                .ForMember(d => d.ProductPinyin, o => o.MapFrom(s => s.Product != null ? s.Product.Pinyin : null));
            CreateMap<CountyAgent, CountyAgentViewModel>();
            CreateMap<CountyAgentPrice, CountyAgentPriceViewModel>();
            CreateMap<CountyDay, CountyDayViewModel>();
            CreateMap<CountyProductSnapshot, CountyProductSnapshotViewModel>();
            CreateMap<CountyOrder, CountyOrderViewModel>()
                .ForMember(d => d.ProductForCountyId, o => o.MapFrom(s => s.CountyProductSnapshot != null ? (int?)s.CountyProductSnapshot.ProductForCountyId : null));
            CreateMap<CountyOrder, CountyOrderSetPriceViewModel>()
                .ForMember(d => d.ProductForCountyId,
                    o => o.MapFrom(s => s.CountyProductSnapshot.ProductForCountyId));
            CreateMap<CountyPromotionSeries, CountyPromotionSeriesViewModel>();
            CreateMap<CountyPromotionSeriesBonus, CountyPromotionSeriesBonusViewModel>();
            CreateMap<CountyPromotionEvent, CountyPromotionEventViewModel>()
                .ForMember(d => d.SeriesName, o => o.MapFrom(s => s.CountyPromotionSeries == null ? null : s.CountyPromotionSeries.Name));
            CreateMap<CountyPromotionEvent, CountyPromotionEventForFullCalendarViewModel>()
                .ForMember(d => d.Title, o => o.MapFrom(s => $"[{s.CountyPromotionSeriesId}] {s.Name}"))
                .ForMember(d => d.Start, o => o.MapFrom(s => s.Date))
                .ForMember(d => d.End, o => o.MapFrom(s => s.Date))
                .ForMember(d => d.AllDay, o => o.MapFrom(s => true))
                .ForMember(d => d.Editable, o => o.MapFrom(s => false));
            CreateMap<CountyPromotionGiftOrder, CountyPromotionGiftOrderViewModel>()
                .ForMember(d => d.ProductForCountyId, o => o.MapFrom(s => s.CountyPromotionEventBonus.ProductForCountyId))
                .ForMember(d => d.CountyPromotionEventId, o => o.MapFrom(s => s.CountyPromotionEventBonus.CountyPromotionEventId));
            CreateMap<CountyOrder, CountyOrderWithGiftViewModel>();

            #endregion

            #region Mall

            CreateMap<ProductForMall, ProductForMallViewModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product == null ? null : src.Product.Name))
                .ForMember(d => d.Order, o => o.MapFrom(s => s.Product != null ? s.Product.Order : s.Order))
                .ForMember(d => d.ProductPinyin, o => o.MapFrom(s => s.Product != null ? s.Product.Pinyin : null));
            CreateMap<MallGroup, MallGroupViewModel>();
            CreateMap<MallCustomer, MallCustomerViewModel>();
            CreateMap<MallDay, MallDayViewModel>();
            CreateMap<MallProductSnapshot, MallProductSnapshotViewModel>();
            CreateMap<MallOrder, MallOrderViewModel>()
                .ForMember(d => d.ProductForMallId, o => o.MapFrom(s => s.MallProductSnapshot != null ? (int?)s.MallProductSnapshot.ProductForMallId : null));
            CreateMap<MallPrice, MallPriceViewModel>();
            CreateMap<MallOrder, MallOrderSetPriceViewModel>()
                .ForMember(d => d.ProductForMallId,
                    o => o.MapFrom(s => s.MallProductSnapshot.ProductForMallId));

            #endregion

            #region Subscription

            CreateMap<ProductForSubscription, ProductForSubscriptionViewModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product == null ? null : src.Product.Name))
                .ForMember(d => d.Order, o => o.MapFrom(s => s.Product != null ? s.Product.Order : s.Order))
                .ForMember(d => d.ProductPinyin, o => o.MapFrom(s => s.Product != null ? s.Product.Pinyin : null));
            CreateMap<SubscriptionDay, SubscriptionDayViewModel>();
            CreateMap<SubscriptionProductSnapshot, SubscriptionProductSnapshotViewModel>();
            CreateMap<SubscriptionPromotionSeries, SubscriptionPromotionSeriesViewModel>();
            CreateMap<SubscriptionPromotionSeriesBonus, SubscriptionPromotionSeriesBonusViewModel>();
            CreateMap<SubscriptionPromotionEvent, SubscriptionPromotionEventViewModel>()
                .ForMember(d => d.SeriesName, o => o.MapFrom(s => s.SubscriptionPromotionSeries == null ? null : s.SubscriptionPromotionSeries.Name));
            CreateMap<SubscriptionPromotionEvent, SubscriptionPromotionEventForFullCalendarViewModel>()
                .ForMember(d => d.Title, o => o.MapFrom(s => $"[{s.SubscriptionPromotionSeriesId}] {s.Name}"))
                .ForMember(d => d.Start, o => o.MapFrom(s => s.Date))
                .ForMember(d => d.End, o => o.MapFrom(s => s.Date))
                .ForMember(d => d.AllDay, o => o.MapFrom(s => true))
                .ForMember(d => d.Editable, o => o.MapFrom(s => false));
            CreateMap<SubscriptionPromotionEventBonus, SubscriptionPromotionEventBonusViewModel>();

            #endregion
        }
    }
}
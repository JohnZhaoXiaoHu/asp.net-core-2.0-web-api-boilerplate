using AutoMapper;
using SalesApi.Models.Collective;
using SalesApi.Models.Overall;
using SalesApi.Models.Retail;
using SalesApi.Models.Settings;
using SalesApi.ViewModels.Collective;
using SalesApi.ViewModels.Overall;
using SalesApi.ViewModels.Retail;
using SalesApi.ViewModels.Settings;

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
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product == null ? null : src.Product.Name));
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

            #endregion

            #region Collective

            CreateMap<ProductForCollective, ProductForCollectiveViewModel>();
            CreateMap<CollectiveCustomer, CollectiveCustomerViewModel>();
            CreateMap<CollectiveDay, CollectiveDayViewModel>();
            CreateMap<CollectiveProductSnapshot, CollectiveProductSnapshotViewModel>();
            CreateMap<CollectiveOrder, CollectiveOrderViewModel>()
                .ForMember(d => d.ProductForCollectiveId, o => o.MapFrom(s => s.CollectiveProductSnapshot != null ? (int?)s.CollectiveProductSnapshot.ProductForCollectiveId : null));
            CreateMap<CollectivePrice, CollectivePriceViewModel>();

            #endregion
        }
    }
}
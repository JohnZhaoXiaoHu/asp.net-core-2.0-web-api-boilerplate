using AutoMapper;
using SalesApi.Models.Retail;
using SalesApi.Models.Settings;
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

            #region Retail

            CreateMap<ProductForRetail, ProductForRetailViewModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product == null ? null : src.Product.Name));
            CreateMap<Retailer, RetailerViewModel>();
            CreateMap<RetailPromotionSeries, RetailPromotionSeriesViewModel>();
            CreateMap<RetailPromotionSeriesBonus, RetailPromotionSeriesBonusViewModel>();
            CreateMap<RetailPromotionEvent, RetailPromotionEventViewModel>();
            CreateMap<RetailPromotionEventBonus, RetailPromotionEventBonusViewModel>();

            #endregion
        }
    }
}
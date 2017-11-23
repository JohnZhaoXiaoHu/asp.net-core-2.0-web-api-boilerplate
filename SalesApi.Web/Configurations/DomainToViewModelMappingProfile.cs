using AutoMapper;
using SalesApi.Models.Settings;
using SalesApi.ViewModels.Settings;

namespace SalesApi.Web.Configurations
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToViewModelMappings";

        public DomainToViewModelMappingProfile()
        {
            CreateMap<Vehicle, VehicleViewModel>();
            CreateMap<DistributionGroup, DistributionGroupViewModel>();
            CreateMap<DeliveryVehicle, DeliveryVehicleViewModel>()
                .ForMember(dest => dest.VehicleName, opt => opt.MapFrom(src => src.Vehicle.Name));
            CreateMap<SubArea, SubAreaViewModel>()
                .ForMember(dest => dest.SalesType, opt => opt.MapFrom(src => src.DeliveryVehicle.SalesType))
                .ForMember(dest => dest.SalesTypeName, opt => opt.MapFrom(src => src.DeliveryVehicle.SalesType.ToString()))
                .ForMember(dest => dest.VehicleName, opt => opt.MapFrom(src => src.DeliveryVehicle.Vehicle.Name));
            CreateMap<SubArea, SubAreaEditViewModel>();
        }
    }
}
using AutoMapper;
using SalesApi.Models.Settings;
using SalesApi.ViewModels.Settings;

namespace SalesApi.Web.Configurations
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => "ViewModelToDomainMappings";

        public ViewModelToDomainMappingProfile()
        {
            CreateMap<VehicleViewModel, Vehicle>();
            CreateMap<DistributionGroupViewModel, DistributionGroup>();
            CreateMap<DeliveryVehicleViewModel, DeliveryVehicle>();
            CreateMap<SubAreaAddViewModel, SubArea>();
            CreateMap<SubAreaEditViewModel, SubArea>();
        }
    }
}
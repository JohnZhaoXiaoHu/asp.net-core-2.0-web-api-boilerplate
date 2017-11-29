using AutoMapper;
using SalesApi.Models.Retail;
using SalesApi.Models.Settings;
using SalesApi.ViewModels.Retail;
using SalesApi.ViewModels.Settings;

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

            #region Retail

            CreateMap<ProductForRetailViewModel, ProductForRetail>();

            #endregion
        }
    }
}
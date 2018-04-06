using AutoMapper;
using SalesApi.Core.DomainModels;
using SalesApi.ViewModels;

namespace SalesApi.Web.Configurations
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => "ViewModelToDomainMappings";

        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductViewModel, Product>();
            CreateMap<ProductCreationViewModel, Product>();
            CreateMap<ProductModificationViewModel, Product>();

            CreateMap<VehicleViewModel, Vehicle>();
            CreateMap<CustomerViewModel, Customer>();
        }
    }
}
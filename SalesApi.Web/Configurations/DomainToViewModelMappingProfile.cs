using AutoMapper;
using SalesApi.Core.DomainModels;
using SalesApi.ViewModels;

namespace SalesApi.Web.Configurations
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToViewModelMappings";

        public DomainToViewModelMappingProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<Vehicle, VehicleViewModel>();
            CreateMap<Customer, CustomerViewModel>();
        }
    }
}
using AutoMapper;
using CoreApi.Models.Angular;
using CoreApi.Models.Core;
using CoreApi.ViewModels.Angular;
using CoreApi.ViewModels.Core;

namespace CoreApi.Web.MyConfigurations
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToViewModelMappings";

        public DomainToViewModelMappingProfile()
        {
            CreateMap<UploadedFile, UploadedFileViewModel>();
            CreateMap<Client, ClientViewModel>();
            CreateMap<Client, ClientModificationViewModel>();
        }
    }
}
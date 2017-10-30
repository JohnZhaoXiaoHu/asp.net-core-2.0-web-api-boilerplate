using AutoMapper;
using CoreApi.Models.Angular;
using CoreApi.Models.Core;
using CoreApi.ViewModels.Angular;
using CoreApi.ViewModels.Core;

namespace CoreApi.Web.MyConfigurations
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => "ViewModelToDomainMappings";

        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UploadedFileViewModel, UploadedFile>();
            CreateMap<ClientViewModel, Client>();
            CreateMap<ClientCreationViewModel, Client>();
            CreateMap<ClientModificationViewModel, Client>();
        }
    }
}
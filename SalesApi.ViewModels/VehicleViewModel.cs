using SalesApi.Core.Abstractions.DomainModels;
using SalesApi.Core.Abstractions.Hateoas;

namespace SalesApi.ViewModels
{
    public class VehicleViewModel: LinkedResourceBaseViewModel
    {
        public string Model { get; set; }
        public string Owner { get; set; }
    }
}
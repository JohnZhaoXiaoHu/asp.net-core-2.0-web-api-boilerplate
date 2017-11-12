using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Settings
{
    public class VehicleViewModel: EntityBase
    {
        public string Name { get; set; }
        public string Owner { get; set; }
    }
}

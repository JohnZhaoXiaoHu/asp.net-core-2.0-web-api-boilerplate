using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Settings
{
    public class SubAreaEditViewModel: IDeleted
    {
        public int DeliveryVehicleId { get; set; }
        public string LegacySubAreaId { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }
}

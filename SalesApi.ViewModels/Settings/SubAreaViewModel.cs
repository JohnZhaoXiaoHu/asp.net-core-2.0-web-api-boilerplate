using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Settings
{
    public class SubAreaViewModel: EntityBase
    {
        public int DeliveryVehicleId { get; set; }
        public string LegacySubAreaId { get; set; }
        public string Name { get; set; }

        public DeliveryVehicleViewModel DeliveryVehicle { get; set; }
    }
}

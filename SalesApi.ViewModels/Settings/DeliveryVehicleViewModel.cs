using Infrastructure.Features.Common;
using SharedSettings.Enums;

namespace SalesApi.ViewModels.Settings
{
    public class DeliveryVehicleViewModel: EntityBase
    {
        public SalesType SalesType { get; set; }
        public string LegacyAreaId { get; set; }
        public int VehicleId { get; set; }
        public int DistributionGroupId { get; set; }

        public string VehicleName { get; set; }
    }
}

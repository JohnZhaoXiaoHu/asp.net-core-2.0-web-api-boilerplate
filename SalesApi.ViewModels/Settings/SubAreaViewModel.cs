using Infrastructure.Features.Common;
using SalesApi.Shared.Enums;

namespace SalesApi.ViewModels.Settings
{
    public class SubAreaViewModel: EntityBase
    {
        public int DeliveryVehicleId { get; set; }
        public string LegacySubAreaId { get; set; }
        public string Name { get; set; }

        public SalesType SalesType { get; set; }
        public string SalesTypeName { get; set; }
        public string VehicleName { get; set; }
    }
}

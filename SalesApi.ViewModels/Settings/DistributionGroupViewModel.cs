using System.Collections.Generic;
using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Settings
{
    public class DistributionGroupViewModel: EntityBase
    {
        public int No { get; set; }
        public string Name { get; set; }
        public ICollection<DeliveryVehicleViewModel> DeliveryVehicles { get; set; }
    }
}

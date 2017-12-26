using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.County
{
    public class CountyOrderViewModel: EntityBase
    {
        public int CountyProductSnapshotId { get; set; }
        public int CountyAgentId { get; set; }
        public int? CountyPromotionEventId { get; set; }

        public string Date { get; set; }
        public string LegacyOrderId { get; set; }
        public int Ordered { get; set; }
        public int Gift { get; set; }
        public decimal Price { get; set; }

        public int? ProductForCountyId { get; set; }
    }
}

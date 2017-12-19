using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Collective
{
    public class CollectiveOrderViewModel: EntityBase
    {
        public int CollectiveProductSnapshotId { get; set; }
        public int CollectiveCustomerId { get; set; }

        public string Date { get; set; }
        public string LegacyOrderId { get; set; }
        public int Ordered { get; set; }
        public int Gift { get; set; }
        public decimal Price { get; set; }
    }
}

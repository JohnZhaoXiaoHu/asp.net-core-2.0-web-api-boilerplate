using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Mall
{
    public class MallOrderViewModel: EntityBase
    {
        public int MallProductSnapshotId { get; set; }
        public int MallCustomerId { get; set; }

        public string Date { get; set; }
        public string LegacyOrderId { get; set; }
        public int Ordered { get; set; }
        public int Gift { get; set; }
        public decimal Price { get; set; }

        public int? ProductForMallId { get; set; }
    }
}

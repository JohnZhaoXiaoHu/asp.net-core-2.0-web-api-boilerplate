using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Retail
{
    public class RetailOrderViewModel: EntityBase
    {
        public int ProductForRetailId { get; set; }
        public int RetailerId { get; set; }
        public int? RetailPromotionEventId { get; set; }

        public string Date { get; set; }
        public string LegacyOrderId { get; set; }
        public int Ordered { get; set; }
        public int Gift { get; set; }
    }
}

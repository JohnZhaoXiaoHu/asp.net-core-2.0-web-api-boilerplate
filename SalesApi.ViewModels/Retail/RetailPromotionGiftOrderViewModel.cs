using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Retail
{
    public class RetailPromotionGiftOrderViewModel: EntityBase
    {
        public int RetailOrderId { get; set; }
        public int RetailPromotionEventBonusId { get; set; }
        public int PromotionGift { get; set; }

        public int ProductForRetailId { get; set; }
        public int RetailPromotionEventId { get; set; }
    }
}

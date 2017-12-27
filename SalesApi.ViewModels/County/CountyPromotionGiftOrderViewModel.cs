using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.County
{
    public class CountyPromotionGiftOrderViewModel: EntityBase
    {
        public int CountyOrderId { get; set; }
        public int CountyPromotionEventBonusId { get; set; }
        public int PromotionGift { get; set; }

        public int ProductForCountyId { get; set; }
        public int CountyPromotionEventId { get; set; }
    }
}

using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Retail
{
    public class RetailPromotionEventBonusViewModel: EntityBase
    {
        public int PromotionEventId { get; set; }
        public int ProductForRetailId { get; set; }
        public int BonusCount { get; set; }
    }
}

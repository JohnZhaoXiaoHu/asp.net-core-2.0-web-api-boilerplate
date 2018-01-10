using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Subscription.Promotion
{
    public class SubscriptionPromotionMonthBonusViewModel: EntityBase
    {
        public int SubscriptionPromotionMonthId { get; set; }
        public int ProductForSubscriptionId { get; set; }
        public int DayBonusCount { get; set; }
    }
}

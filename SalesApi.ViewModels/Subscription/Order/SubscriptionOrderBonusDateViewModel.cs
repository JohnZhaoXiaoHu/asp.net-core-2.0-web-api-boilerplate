using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Subscription.Order
{
    public class SubscriptionOrderBonusDateViewModel: EntityBase
    {
        public int SubscriptionOrderId { get; set; }
        public int SubscriptionMonthPromotionBonusDateId { get; set; }
        public int DayBonusCount { get; set; }
    }
}

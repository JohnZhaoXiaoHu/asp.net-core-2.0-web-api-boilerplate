using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Subscription.Promotion
{
    public class SubscriptionPromotionMonthViewModel: EntityBase
    {
        public int SubscriptionPromotionId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}

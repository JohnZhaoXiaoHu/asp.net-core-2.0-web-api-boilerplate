using System.Collections.Generic;

namespace SalesApi.ViewModels.Subscription.Promotion
{
    public class SubscriptionPromotionMonthAddViewModel
    {
        public int SubscriptionPromotionId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public List<SubscriptionPromotionMonthBonusAddViewModel> SubscriptionPromotionMonthBonuses { get; set; }
    }
}

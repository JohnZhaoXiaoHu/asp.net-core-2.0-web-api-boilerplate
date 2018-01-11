using System.Collections.Generic;

namespace SalesApi.ViewModels.Subscription.Promotion
{
    public class SubscriptionPromotionMonthWithBonusViewModel: SubscriptionPromotionMonthViewModel
    {
        public SubscriptionPromotionViewModel SubscriptionPromotion { get; set; }
        public List<SubscriptionPromotionMonthBonusViewModel> SubscriptionPromotionMonthBonuses { get; set; }
    }
}

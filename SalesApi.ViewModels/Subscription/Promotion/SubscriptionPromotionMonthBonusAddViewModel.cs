using System.Collections.Generic;

namespace SalesApi.ViewModels.Subscription.Promotion
{
    public class SubscriptionPromotionMonthBonusAddViewModel
    {
        public int SubscriptionPromotionMonthId { get; set; }
        public int ProductForSubscriptionId { get; set; }
        public int DayBonusCount { get; set; }
        public List<SubscriptionPromotionMonthBonusDeliveryDateAddViewModel> SubscriptionPromotionMonthBonusDeliveryDates { get; set; }
    }
}

using System;

namespace SalesApi.ViewModels.Subscription.Promotion
{
    public class SubscriptionPromotionMonthBonusDeliveryDateAddViewModel
    {
        public int SubscriptionPromotionMonthBonusId { get; set; }
        public DateTime Date { get; set; }
        public int DayBonusCount { get; set; }
    }
}

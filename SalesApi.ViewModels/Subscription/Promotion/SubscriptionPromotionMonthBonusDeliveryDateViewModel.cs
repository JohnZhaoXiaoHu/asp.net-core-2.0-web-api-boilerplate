using System;
using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Subscription.Promotion
{
    public class SubscriptionPromotionMonthBonusDeliveryDateViewModel: EntityBase
    {
        public int SubscriptionPromotionMonthBonusId { get; set; }
        public DateTime Date { get; set; }
        public int DayBonusCount { get; set; }
    }
}

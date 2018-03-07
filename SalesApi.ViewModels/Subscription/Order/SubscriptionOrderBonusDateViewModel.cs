using System;
using Infrastructure.Features.Common;
using SalesApi.ViewModels.Subscription.Promotion;

namespace SalesApi.ViewModels.Subscription.Order
{
    public class SubscriptionOrderBonusDateViewModel: EntityBase
    {
        public int SubscriptionOrderId { get; set; }
        public int SubscriptionMonthPromotionBonusDateId { get; set; }
        public DateTime Date { get; set; }
        public int DayBonusCount { get; set; }
        public int ProductForSubscriptionId { get; set; }
        public string ProductName { get; set; }
        public SubscriptionMonthPromotionSimpleViewModel SubscriptionMonthPromotion { get; set; }
        
    }
}

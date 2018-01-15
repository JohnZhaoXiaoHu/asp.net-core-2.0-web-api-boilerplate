using System;
using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Subscription.Promotion
{
    public class SubscriptionMonthPromotionBonusDateViewModel: EntityBase
    {
        public int SubscriptionMonthPromotionBonusId { get; set; }
        public DateTime Date { get; set; }
        public int DayBonusCount { get; set; }
    }
}

using System;

namespace SalesApi.ViewModels.Subscription.Order
{
    public class SubscriptionOrderModifiedBonusDateAddViewModel
    {
        public int SubscriptionOrderId { get; set; }
        public int SubscriptionProductSnapshotId { get; set; }
        public DateTime Date { get; set; }
        public int DayCount { get; set; }
    }
}

using System;
using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Subscription.Order
{
    public class SubscriptionOrderModifiedBonusDateViewModel: EntityBase
    {
        public int SubscriptionOrderId { get; set; }
        public int SubscriptionProductSnapshotId { get; set; }
        public DateTime Date { get; set; }
        public int DayCount { get; set; }
        public string ProductName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApi.ViewModels.Subscription.Order
{
    public class SubscriptionOrderAddViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int MilkmanId { get; set; }
        public int SubscriptionProductSnapshotId { get; set; }
        public int? SubscriptionMonthPromotionId { get; set; }
        public int PresetDayCount { get; set; }
        public int PresetDayGift { get; set; } // 批条

        public List<SubscriptionOrderDateViewModel> SubscriptionOrderDates { get; set; }
        public List<SubscriptionOrderBonusDateViewModel> SubscriptionOrderBonusDates { get; set; }
    }
}

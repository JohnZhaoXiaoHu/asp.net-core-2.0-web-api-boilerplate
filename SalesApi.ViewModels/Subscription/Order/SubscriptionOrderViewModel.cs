using System;
using System.Collections.Generic;
using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Subscription.Order
{
    public class SubscriptionOrderViewModel: EntityBase
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int MilkmanId { get; set; }
        public int SubscriptionProductSnapshotId { get; set; }
        public int? SubscriptionMonthPromotionId { get; set; }
        public int PresetDayCount { get; set; }
        public int PresetDayGift { get; set; } // 批条

        public DateTime? PaidTime { get; set; }

        public string MilkmanName { get; set; }
        public string MilkmanNo { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public List<SubscriptionOrderDateViewModel> SubscriptionOrderDates { get; set; }
        public List<SubscriptionOrderBonusDateViewModel> SubscriptionOrderBonusDates { get; set; }
        public List<SubscriptionOrderModifiedBonusDateViewModel> SubscriptionOrderModifiedBonusDates { get; set; }
    }
}

using System.Collections.Generic;
using SalesApi.ViewModels.Subscription.Promotion;

namespace SalesApi.ViewModels.Subscription.Order
{
    public class SubscriptionOrderAddViewModel
    {
        public int MilkmanId { get; set; }
        public int SubscriptionProductSnapshotId { get; set; }
        public int? SubscriptionMonthPromotionId { get; set; }
        public int PresetDayCount { get; set; }
        public int PresetDayGift { get; set; } // 批条
        
        public List<SubscriptionMonthPromotionViewModel> Promotions { get; set; }
    }
}

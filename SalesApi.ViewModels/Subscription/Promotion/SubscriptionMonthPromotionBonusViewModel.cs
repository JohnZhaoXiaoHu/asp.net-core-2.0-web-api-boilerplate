using System.Collections.Generic;
using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Subscription.Promotion
{
    public class SubscriptionMonthPromotionBonusViewModel: EntityBase
    {
        public int SubscriptionMonthPromotionId { get; set; }
        public int ProductForSubscriptionId { get; set; }
        public int PresetDayBonusCount { get; set; }

        public List<SubscriptionMonthPromotionBonusDateViewModel> SubscriptionMonthPromotionBonusDates { get; set; }
    }
}

using System.Collections.Generic;
using Infrastructure.Features.Common;
using SalesApi.Shared.Enums;

namespace SalesApi.ViewModels.Subscription.Promotion
{
    public class SubscriptionMonthPromotionViewModel: EntityBase
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string Name { get; set; }
        public SubscriptionPromotionType PromotionType { get; set; }
        public string PromotionTypeDisplay => PromotionType.ToString();
        public int? StartDate { get; set; }
        public int? EndDate { get; set; }
        public int ProductForSubscriptionId { get; set; }
        public int DayBase { get; set; }

        public List<SubscriptionMonthPromotionBonusViewModel> SubscriptionMonthPromotionBonuses { get; set; }

        public string Description { get; set; }
    }
}

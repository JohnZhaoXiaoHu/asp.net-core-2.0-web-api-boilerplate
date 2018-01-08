using System.ComponentModel.DataAnnotations;
using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Subscription
{
    public class SubscriptionPromotionEventBonusViewModel: EntityBase
    {
        public int SubscriptionPromotionEventId { get; set; }
        public int ProductForSubscriptionId { get; set; }

        [Display(Name = "赠品数")]
        [Range(1, int.MaxValue, ErrorMessage = "{0}的必须大于{1}")]
        public int BonusCount { get; set; }
    }
}

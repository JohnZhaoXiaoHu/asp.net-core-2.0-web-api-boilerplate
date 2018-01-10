using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Subscription.Promotion
{
    public class SubscriptionPromotionViewModel : EntityBase
    {
        public string Name { get; set; }
        public int? StartDate { get; set; }
        public int? EndDate { get; set; }
        public int MonthSpan { get; set; }
        public int ProductForSubscriptionId { get; set; }
        public int DayBase { get; set; }
    }
}

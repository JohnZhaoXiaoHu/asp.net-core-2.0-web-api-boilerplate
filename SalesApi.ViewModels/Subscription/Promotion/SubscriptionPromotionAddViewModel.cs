using Microsoft.EntityFrameworkCore;
using SalesApi.Shared.Enums;

namespace SalesApi.ViewModels.Subscription.Promotion
{
    public class SubscriptionPromotionAddViewModel
    {
        public string Name { get; set; }
        public SubscriptionPromotionType PromotionType { get; set; }
        public int? StartDate { get; set; }
        public int? EndDate { get; set; }
        public int MonthSpan { get; set; }
        public int ProductForSubscriptionId { get; set; }
        public int DayBase { get; set; }

        public DbSet<SubscriptionPromotionMonthAddViewModel> SubscriptionPromotionMonths { get; set; }
    }
}

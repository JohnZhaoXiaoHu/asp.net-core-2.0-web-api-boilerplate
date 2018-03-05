using System;

namespace SalesApi.ViewModels.Subscription.Order
{
    public class SubscriptionOrderDayCountErrorViewModel
    {
        public DateTime Date { get; set; }
        public int ProductForSubscriptionId { get; set; }
        public string ProductName { get; set; }
        public int DayCount { get; set; }
        public string Error => $"{Date:yyyy-MM-dd} {ProductName} 数量为 {DayCount}";
    }
}

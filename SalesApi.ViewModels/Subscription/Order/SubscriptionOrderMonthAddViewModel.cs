using System.Collections.Generic;

namespace SalesApi.ViewModels.Subscription.Order
{
    public class SubscriptionOrderMonthAddViewModel
    {
        public string Month { get; set; }
        public List<SubscriptionOrderDateAddViewModel> Days { get; set; }
    }
}

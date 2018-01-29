using System;
using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Subscription.Order
{
    public class SubscriptionOrderDateAddViewModel : ISelectable
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime Date { get; set; }
        public bool IsWeekend { get; set; }
        public bool Selected { get; set; }
    }
}

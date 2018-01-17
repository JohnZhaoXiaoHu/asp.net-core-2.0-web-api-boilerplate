using System;
using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Subscription.Order
{
    public class SubscriptionOrderDateViewModel: EntityBase
    {
        public int SubscriptionOrderId { get; set; }
        public DateTime Date { get; set; }
    }
}

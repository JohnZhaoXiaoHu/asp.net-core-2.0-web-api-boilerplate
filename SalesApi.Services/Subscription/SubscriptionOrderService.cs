using System.Collections.Generic;
using SalesApi.Repositories.Subscription.Order;
using SalesApi.ViewModels.Subscription.Order;

namespace SalesApi.Services.Subscription
{
    public interface ISubscriptionOrderService
    {
        void AddSubscriptionOrder(List<SubscriptionOrderAddViewModel> vms, List<SubscriptionOrderMonthAddViewModel> months, string userName);
    }

    public class SubscriptionOrderService: ISubscriptionOrderService
    {
        private readonly ISubscriptionOrderRepository _subscriptionOrderRepository;

        public SubscriptionOrderService(ISubscriptionOrderRepository subscriptionOrderRepository)
        {
            _subscriptionOrderRepository = subscriptionOrderRepository;
        }

        public void AddSubscriptionOrder(List<SubscriptionOrderAddViewModel> vms, List<SubscriptionOrderMonthAddViewModel> months, string userName)
        {
            
        }
    }
}

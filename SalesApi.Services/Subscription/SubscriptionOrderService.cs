using System.Collections.Generic;
using AutoMapper;
using Infrastructure.Features.Common;
using SalesApi.Models.Subscription.Order;
using SalesApi.Repositories.Subscription.Order;
using SalesApi.ViewModels.Subscription.Order;

namespace SalesApi.Services.Subscription
{
    public interface ISubscriptionOrderService
    {
        void AddSubscriptionOrder(List<SubscriptionOrderAddViewModel> vms, string userName);
    }

    public class SubscriptionOrderService: ISubscriptionOrderService
    {
        private readonly ISubscriptionOrderRepository _subscriptionOrderRepository;

        public SubscriptionOrderService(ISubscriptionOrderRepository subscriptionOrderRepository)
        {
            _subscriptionOrderRepository = subscriptionOrderRepository;
        }

        public void AddSubscriptionOrder(List<SubscriptionOrderAddViewModel> vms, string userName)
        {
            var orders = Mapper.Map<List<SubscriptionOrder>>(vms);
            foreach (var order in orders)
            {
                order.SetCreation(userName);
                foreach (var orderDate in order.SubscriptionOrderDates)
                {
                    orderDate.SetCreation(userName);
                }
                foreach (var bonusDate in order.SubscriptionOrderBonusDates)
                {
                    bonusDate.SetCreation(userName);
                }
            }
            _subscriptionOrderRepository.AddRange(orders);
        }
    }
}

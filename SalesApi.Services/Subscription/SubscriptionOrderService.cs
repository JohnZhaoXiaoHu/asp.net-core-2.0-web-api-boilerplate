using System;
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
        void AddSubscriptionOrders(List<SubscriptionOrderAddViewModel> vms, string userName);
    }

    public class SubscriptionOrderService: ISubscriptionOrderService
    {
        private readonly ISubscriptionOrderRepository _subscriptionOrderRepository;

        public SubscriptionOrderService(ISubscriptionOrderRepository subscriptionOrderRepository)
        {
            _subscriptionOrderRepository = subscriptionOrderRepository;
        }

        public void AddSubscriptionOrders(List<SubscriptionOrderAddViewModel> vms, string userName)
        {
            var now = DateTime.Now;
            var orders = Mapper.Map<List<SubscriptionOrder>>(vms);
            foreach (var order in orders)
            {
                order.SetCreation(userName);
                order.CreateTime = order.UpdateTime = now;
                foreach (var orderDate in order.SubscriptionOrderDates)
                {
                    orderDate.SetCreation(userName);
                    orderDate.CreateTime = orderDate.UpdateTime = now;
                }
                foreach (var bonusDate in order.SubscriptionOrderBonusDates)
                {
                    bonusDate.SetCreation(userName);
                    bonusDate.CreateTime = bonusDate.UpdateTime = now;
                }
            }
            _subscriptionOrderRepository.AddRange(orders);
        }
    }
}

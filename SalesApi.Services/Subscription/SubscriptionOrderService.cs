using System;
using System.Collections.Generic;
using System.Linq;
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
        void ValidateOrderDatesAndModifiedBonusDates(List<SubscriptionOrderAddViewModel> vms, DateTime today, DateTime tomorrow, bool hasSubscriptionDayBeenConfirmed);
        void ValidateOrderBonusDates(List<DateTime> dates, DateTime today, DateTime tomorrow, bool hasSubscriptionDayBeenConfirmed);
    }

    public class SubscriptionOrderService: ISubscriptionOrderService
    {
        private readonly ISubscriptionOrderRepository _subscriptionOrderRepository;

        public SubscriptionOrderService(ISubscriptionOrderRepository subscriptionOrderRepository)
        {
            _subscriptionOrderRepository = subscriptionOrderRepository;
        }

        public void ValidateOrderDatesAndModifiedBonusDates(List<SubscriptionOrderAddViewModel> vms, DateTime today, DateTime tomorrow, bool hasSubscriptionDayBeenConfirmed)
        {
            var orderDates = vms.SelectMany(x => x.SubscriptionOrderDates).Select(x => x.Date).Distinct().ToList();
            var minOrderDate = orderDates.Min();
            if (minOrderDate <= today)
            {
                throw new Exception("订单日期不得小于明天");
            }
            if (minOrderDate <= tomorrow && hasSubscriptionDayBeenConfirmed)
            {
                throw new Exception("今日专送已报货，订单不得小于后天");
            }
            var modifiedBonusDates = vms.SelectMany(x => x.SubscriptionOrderModifiedBonusDates).Select(x => x.Date)
                .Distinct().ToList();
            var minModifiedBonusDate = modifiedBonusDates.Min();
            if (minModifiedBonusDate <= today)
            {
                throw new Exception("手动赠品派送日期不得小于明天");
            }
            if (minModifiedBonusDate <= tomorrow && hasSubscriptionDayBeenConfirmed)
            {
                throw new Exception("今日专送已报货，手动赠品派送日期不得小于后天");
            }
        }

        public void ValidateOrderBonusDates(List<DateTime> dates, DateTime today, DateTime tomorrow, bool hasSubscriptionDayBeenConfirmed)
        {
            var minBonusDate = dates.Min();
            if (minBonusDate <= today)
            {
                throw new Exception("活动赠品派送日期不得小于明天");
            }
            if (minBonusDate <= tomorrow && hasSubscriptionDayBeenConfirmed)
            {
                throw new Exception("今日专送已报货，活动赠品派送日期不得小于后天");
            }
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
                foreach (var modifiedBonusDate in order.SubscriptionOrderModifiedBonusDates)
                {
                    modifiedBonusDate.SetCreation(userName);
                    modifiedBonusDate.CreateTime = modifiedBonusDate.UpdateTime = now;
                }
            }
            _subscriptionOrderRepository.AddRange(orders);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Subscription.Order;
using SalesApi.Models.Subscription.Promotion;
using SalesApi.Repositories.Subscription;
using SalesApi.Repositories.Subscription.Order;
using SalesApi.Repositories.Subscription.Promotion;
using SalesApi.ViewModels.Subscription.Order;

namespace SalesApi.Services.Subscription
{
    public interface ISubscriptionOrderService
    {
        void AddSubscriptionOrders(List<SubscriptionOrderAddViewModel> vms, string userName);
        void ValidateOrderDatesAndModifiedBonusDates(List<SubscriptionOrderAddViewModel> vms, DateTime today, DateTime tomorrow, bool hasSubscriptionDayBeenConfirmed);
        void ValidateOrderBonusDates(List<DateTime> dates, DateTime today, DateTime tomorrow, bool hasSubscriptionDayBeenConfirmed);
        Task ValidateDayCountAsync(List<SubscriptionOrderAddViewModel> vms);
    }

    public class SubscriptionOrderService : ISubscriptionOrderService
    {
        private readonly ISubscriptionOrderRepository _subscriptionOrderRepository;
        private readonly ISubscriptionOrderDateRepository _subscriptionOrderDateRepository;
        private readonly ISubscriptionMonthPromotionBonusDateRepository _subscriptionMonthPromotionBonusDateRepository;
        private readonly ISubscriptionOrderModifiedBonusDateRepository _subscriptionOrderModifiedBonusDateRepository;
        private readonly ISubscriptionProductSnapshotRepository _subscriptionProductSnapshotRepository;

        public SubscriptionOrderService(
            ISubscriptionOrderRepository subscriptionOrderRepository,
            ISubscriptionOrderDateRepository subscriptionOrderDateRepository,
            ISubscriptionMonthPromotionBonusDateRepository subscriptionMonthPromotionBonusDateRepository,
            ISubscriptionOrderModifiedBonusDateRepository subscriptionOrderModifiedBonusDateRepository,
            ISubscriptionProductSnapshotRepository subscriptionProductSnapshotRepository)
        {
            _subscriptionOrderRepository = subscriptionOrderRepository;
            _subscriptionOrderDateRepository = subscriptionOrderDateRepository;
            _subscriptionMonthPromotionBonusDateRepository = subscriptionMonthPromotionBonusDateRepository;
            _subscriptionOrderModifiedBonusDateRepository = subscriptionOrderModifiedBonusDateRepository;
            _subscriptionProductSnapshotRepository = subscriptionProductSnapshotRepository;
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

        public async Task ValidateDayCountAsync(List<SubscriptionOrderAddViewModel> vms)
        {
            // dates
            var vmOrderDates = vms.SelectMany(x => x.SubscriptionOrderDates).Select(x => x.Date).ToList();
            var vmModifiedBonusDates = vms.SelectMany(x => x.SubscriptionOrderModifiedBonusDates).Select(x => x.Date)
                .Distinct().ToList();
            var vmOrderAndModifiedDates = vmOrderDates.Concat(vmModifiedBonusDates).ToList();
            var vmBonusDateIds = vms.SelectMany(x => x.SubscriptionOrderBonusDates)
                .Select(x => x.SubscriptionMonthPromotionBonusDateId).Distinct().ToList();
            var promotionBonusDateModels = await _subscriptionMonthPromotionBonusDateRepository.AllIncluding(x => x.SubscriptionMonthPromotionBonus)
                .Where(x => vmBonusDateIds.Contains(x.Id)).ToListAsync();
            var bonusDates = promotionBonusDateModels.Select(x => x.Date).ToList();
            var allDates = vmOrderAndModifiedDates.Concat(bonusDates).Distinct().OrderBy(x => x).ToList();

            // products
            var todayStr = DateTime.Now.Date.ToString("yyyy-MM-dd");
            var todayProductSnapshots = await _subscriptionProductSnapshotRepository.All
                .Where(x => x.SubscriptionDay.Date == todayStr).ToListAsync();

            // db models
            var dbOrders = await _subscriptionOrderDateRepository.AllIncluding(x => x.SubscriptionOrder)
                .Where(x => allDates.Contains(x.Date)).ToListAsync();
            var dbBonusOrders = await _subscriptionMonthPromotionBonusDateRepository.AllIncluding(x => x.SubscriptionMonthPromotionBonus)
                .Where(x => allDates.Contains(x.Date)).ToListAsync();
            var dbModifiedBonusOrders = await _subscriptionOrderModifiedBonusDateRepository.All
                .Where(x => allDates.Contains(x.Date)).ToListAsync();

            // validation by date then by product
            foreach (var date in allDates)
            {
                var vmDateOrders = vms.SelectMany(x => x.SubscriptionOrderDates).Where(x => x.Date == date)
                    .ToList();
                var dateOrderProductSnapshotIds = vms.Where(x => x.SubscriptionOrderDates.Any(y => y.Date == date))
                    .Select(x => x.SubscriptionProductSnapshotId).ToList();

                var dbDateOrders = dbOrders.Where(x => x.Date == date).ToList();
                var dbOrderProductSnapshotIds = dbDateOrders.Select(x => x.SubscriptionOrder.SubscriptionProductSnapshotId)
                    .ToList();

                var dbDateBonusOrders = dbBonusOrders.Where(x => x.Date == date).ToList();
                // this is subscription for product id.
                var dbDateBonusOrderProductIds = dbDateBonusOrders
                    .Select(x => x.SubscriptionMonthPromotionBonus.ProductForSubscriptionId).ToList();

                var dbDateModifiedBonusOrders = dbModifiedBonusOrders.Where(x => x.Date == date).ToList();
                var dbDateModifiedBonusOrderProductISnaphostds =
                    dbDateModifiedBonusOrders.Select(x => x.SubscriptionProductSnapshotId).ToList();
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

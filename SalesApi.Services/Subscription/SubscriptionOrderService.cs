using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Subscription.Order;
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
        Task<List<SubscriptionOrderValidationViewModel>> ValidateDayCountAsync(int milkmanId, List<SubscriptionOrderAddViewModel> orderVms);
    }

    public class SubscriptionOrderService : ISubscriptionOrderService
    {
        private readonly ISubscriptionOrderRepository _subscriptionOrderRepository;
        private readonly ISubscriptionOrderDateRepository _subscriptionOrderDateRepository;
        private readonly ISubscriptionMonthPromotionBonusDateRepository _subscriptionMonthPromotionBonusDateRepository;
        private readonly ISubscriptionOrderModifiedBonusDateRepository _subscriptionOrderModifiedBonusDateRepository;
        private readonly ISubscriptionProductSnapshotRepository _subscriptionProductSnapshotRepository;
        private readonly IProductForSubscriptionRepository _productForSubscriptionRepository;
        private readonly ISubscriptionOrderBonusDateRepository _subscriptionOrderBonusDateRepository;

        public SubscriptionOrderService(
            ISubscriptionOrderRepository subscriptionOrderRepository,
            ISubscriptionOrderDateRepository subscriptionOrderDateRepository,
            ISubscriptionMonthPromotionBonusDateRepository subscriptionMonthPromotionBonusDateRepository,
            ISubscriptionOrderModifiedBonusDateRepository subscriptionOrderModifiedBonusDateRepository,
            ISubscriptionProductSnapshotRepository subscriptionProductSnapshotRepository,
            IProductForSubscriptionRepository productForSubscriptionRepository,
            ISubscriptionOrderBonusDateRepository subscriptionOrderBonusDateRepository)
        {
            _subscriptionOrderRepository = subscriptionOrderRepository;
            _subscriptionOrderDateRepository = subscriptionOrderDateRepository;
            _subscriptionMonthPromotionBonusDateRepository = subscriptionMonthPromotionBonusDateRepository;
            _subscriptionOrderModifiedBonusDateRepository = subscriptionOrderModifiedBonusDateRepository;
            _subscriptionProductSnapshotRepository = subscriptionProductSnapshotRepository;
            _productForSubscriptionRepository = productForSubscriptionRepository;
            _subscriptionOrderBonusDateRepository = subscriptionOrderBonusDateRepository;
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
            if (modifiedBonusDates.Any())
            {
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

        public async Task<List<SubscriptionOrderValidationViewModel>> ValidateDayCountAsync(int milkmanId, List<SubscriptionOrderAddViewModel> orderVms)
        {
            // dates
            foreach (var orderVm in orderVms)
            {
                foreach (var orderDate in orderVm.SubscriptionOrderDates)
                {
                    orderDate.Date = orderDate.Date.ToLocalTime();
                }
                foreach (var modifiedBonusDate in orderVm.SubscriptionOrderModifiedBonusDates)
                {
                    modifiedBonusDate.Date = modifiedBonusDate.Date.ToLocalTime();
                }
            }
            var vmOrderDates = orderVms.SelectMany(x => x.SubscriptionOrderDates).Select(x => x.Date).ToList();
            var vmModifiedBonusOrders = orderVms.SelectMany(x => x.SubscriptionOrderModifiedBonusDates).ToList();
            var vmModifiedBonusDates = vmModifiedBonusOrders.Select(x => x.Date)
                .Distinct().ToList();
            var vmOrderAndModifiedDates = vmOrderDates.Concat(vmModifiedBonusDates).ToList();
            var vmPromotionDateIds = orderVms.SelectMany(x => x.SubscriptionOrderBonusDates)
                .Select(x => x.SubscriptionMonthPromotionBonusDateId).Distinct().ToList();
            var promotionOrdersForVm = await _subscriptionMonthPromotionBonusDateRepository.AllIncluding(x => x.SubscriptionMonthPromotionBonus)
                .Where(x => vmPromotionDateIds.Contains(x.Id)).ToListAsync();
            var vmPromotionDates = promotionOrdersForVm.Select(x => x.Date).ToList();
            var allDates = vmOrderAndModifiedDates.Concat(vmPromotionDates).Distinct().OrderBy(x => x).ToList();

            // db models
            var dbOrders = await _subscriptionOrderDateRepository.AllIncluding(x => x.SubscriptionOrder)
                .Where(x => x.SubscriptionOrder.MilkmanId == milkmanId && allDates.Contains(x.Date)).ToListAsync();
            var dbPromotionOrders = await _subscriptionOrderBonusDateRepository.GetWithGrandPromotionInformation(milkmanId, allDates);
            var dbModifiedBonusOrders = await _subscriptionOrderModifiedBonusDateRepository.All
                .Where(x => x.SubscriptionOrder.MilkmanId == milkmanId && allDates.Contains(x.Date)).ToListAsync();

            // products
            var vmPromotionProductIds = promotionOrdersForVm
                .Select(x => x.SubscriptionMonthPromotionBonus.ProductForSubscriptionId).ToList();

            var dbOrderProductSnapshotIds =
                dbOrders.Select(x => x.SubscriptionOrder.SubscriptionProductSnapshotId).ToList();
            var dbModifiedBonusProductSnapshotIds =
                dbModifiedBonusOrders.Select(x => x.SubscriptionProductSnapshotId).ToList();
            var dbPromotionProductIds = dbPromotionOrders
                .Select(x => x.SubscriptionMonthPromotionBonus.ProductForSubscriptionId).ToList();

            var dbProductSnapshotIds = dbOrderProductSnapshotIds.Concat(dbModifiedBonusProductSnapshotIds).Distinct().ToList();
            var productIds = vmPromotionProductIds.Concat(dbPromotionProductIds).Distinct().ToList();

            var dbProductSnapshots = await _subscriptionProductSnapshotRepository.All
                .Where(x => dbProductSnapshotIds.Contains(x.Id)).ToListAsync();

            var allProductIds = productIds.Concat(dbProductSnapshots.Select(x => x.ProductForSubscriptionId)).Distinct().ToList();
            var allProducts = await _productForSubscriptionRepository.AllIncluding(x => x.Product).Where(x => allProductIds.Contains(x.Id))
                .ToListAsync();

            var todayStr = DateTime.Now.Date.ToString("yyyy-MM-dd");
            var todayProductSnapshots = await _subscriptionProductSnapshotRepository.All
                .Where(x => x.SubscriptionDay.Date == todayStr).ToListAsync();

            // validation by date then by product
            var errors = new List<SubscriptionOrderValidationViewModel>();
            foreach (var date in allDates)
            {
                foreach (var productId in allProductIds)
                {
                    int count;
                    var vmDatePromotionOrders = promotionOrdersForVm.Where(x => x.Date == date && x.SubscriptionMonthPromotionBonus.ProductForSubscriptionId == productId).ToList();
                    var dbDatePromotionOrders = dbPromotionOrders.Where(x => x.Date == date && x.SubscriptionMonthPromotionBonus.ProductForSubscriptionId == productId).ToList();
                    count = vmDatePromotionOrders.Sum(x => x.DayBonusCount) +
                            dbDatePromotionOrders.Sum(x => x.DayBonusCount);

                    var todayProductSnapshot = todayProductSnapshots.SingleOrDefault(x => x.ProductForSubscriptionId == productId);
                    if (todayProductSnapshot != null) // 可能有order 和 modifiedBonusOrder
                    {
                        var todayProductSnapshotId = todayProductSnapshot.Id;
                        var vmDateOrders = orderVms.Where(x => x.SubscriptionProductSnapshotId == todayProductSnapshotId)
                            .SelectMany(x => x.SubscriptionOrderDates).Where(x => x.Date == date).ToList();
                        foreach (var vmDateOrder in vmDateOrders)
                        {
                            var parentOrder = orderVms.Single(x => x.SubscriptionOrderDates.Contains(vmDateOrder));
                            count += parentOrder.PresetDayCount + parentOrder.PresetDayGift;
                        }
                        var vmDateModifiedOrders = vmModifiedBonusOrders.Where(x => x.Date == date && x.SubscriptionProductSnapshotId == todayProductSnapshotId).ToList();
                        count += vmDateModifiedOrders.Sum(x => x.DayCount);
                    }
                    var productSnapshotIdsForDb =
                        dbProductSnapshots.Where(x => x.ProductForSubscriptionId == productId).Select(x => x.Id).ToList();
                    if (productSnapshotIdsForDb.Any())
                    {
                        var dbDateOrders = dbOrders.Where(x => x.Date == date && productSnapshotIdsForDb.Contains(x.SubscriptionOrder.SubscriptionProductSnapshotId)).ToList();
                        count += dbDateOrders.Sum(x => x.SubscriptionOrder.PresetDayCount + x.SubscriptionOrder.PresetDayGift);
                        var dbDateModifiedBonusOrders = dbModifiedBonusOrders.Where(x => x.Date == date && productSnapshotIdsForDb.Contains(x.SubscriptionProductSnapshotId)).ToList();
                        count += dbDateModifiedBonusOrders.Sum(x => x.DayCount);
                    }

                    if (count < 0)
                    {
                        var product = allProducts.Single(x => x.Id == productId);
                        errors.Add(new SubscriptionOrderValidationViewModel
                        {
                            Date = date,
                            DayCount = count,
                            ProductForSubscriptionId = productId,
                            ProductName = product.Product.Name
                        });
                    }
                }
            }
            return errors;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SalesApi.Models.Subscription.Order;
using SalesApi.Repositories.Subscription.Order;
using SalesApi.Repositories.Subscription.Promotion;
using SalesApi.Services.Subscription;
using SalesApi.ViewModels.Subscription.Order;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Subscription.Order
{
    [Route("api/sales/[controller]")]
    public class SubscriptionOrderController : SubscriptionController<SubscriptionOrderController>
    {
        private readonly ISubscriptionOrderRepository _subscriptionOrderRepository;
        private readonly ISubscriptionOrderService _subscriptionOrderService;
        private readonly ISubscriptionMonthPromotionBonusDateRepository _subscriptionMonthPromotionBonusDateRepository;

        public SubscriptionOrderController(
            ISubscriptionService<SubscriptionOrderController> subscriptionService,
            ISubscriptionOrderRepository subscriptionOrderRepository,
            ISubscriptionOrderService subscriptionOrderService, ISubscriptionMonthPromotionBonusDateRepository subscriptionMonthPromotionBonusDateRepository) : base(subscriptionService)
        {
            _subscriptionOrderRepository = subscriptionOrderRepository;
            _subscriptionOrderService = subscriptionOrderService;
            _subscriptionMonthPromotionBonusDateRepository = subscriptionMonthPromotionBonusDateRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JToken jObj)
        {
            var orderVms = jObj["orders"].ToObject<List<SubscriptionOrderAddViewModel>>();
            var milkmanId = jObj["milkmanId"].ToObject<int>();
            if (!orderVms.Any())
            {
                return BadRequest();
            }
            if (!TryValidateModel(orderVms))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }
            var hasDayBeenConfirmed = await HasSubscriptionDayBeenConfirmed();

            var monthPromotionIds = orderVms.Select(x => x.SubscriptionMonthPromotionId).Distinct().ToList();
            var promotionBonusDates = await _subscriptionMonthPromotionBonusDateRepository.AllIncluding(x => x.SubscriptionMonthPromotionBonus)
                .Where(x => monthPromotionIds.Contains(x.SubscriptionMonthPromotionBonus.SubscriptionMonthPromotion.Id)).ToListAsync();
            if (promotionBonusDates.Any())
            {
                var bonusDates = promotionBonusDates.Select(x => x.Date).ToList();
                _subscriptionOrderService.ValidateOrderBonusDates(bonusDates, Today, Tomorrow, hasDayBeenConfirmed);
                foreach (var orderVm in orderVms)
                {
                    if (orderVm.SubscriptionMonthPromotionId.HasValue)
                    {
                        orderVm.SubscriptionOrderBonusDates = promotionBonusDates.Where(x => x.SubscriptionMonthPromotionBonus.SubscriptionMonthPromotionId == orderVm.SubscriptionMonthPromotionId)
                        .Select(x => new SubscriptionOrderBonusDateViewModel { SubscriptionMonthPromotionBonusDateId = x.Id })
                        .ToList();
                    }
                }
            }

            _subscriptionOrderService.ValidateOrderDatesAndModifiedBonusDates(orderVms, Today, Tomorrow, hasDayBeenConfirmed);
            var invalidateDates = await _subscriptionOrderService.ValidateDayCountAsync(milkmanId, orderVms);
            if (invalidateDates.Any())
            {
                return BadRequest(invalidateDates);
            }
            var createTime = _subscriptionOrderService.AddSubscriptionOrders(orderVms, UserName);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            return Ok(createTime);
        }

        [HttpGet]
        [Route("ByCreateTime/{createTime}")]
        public async Task<IActionResult> GetyCreateTime(DateTime createTime)
        {
            var orders = await _subscriptionOrderRepository.GetByCreateTimeAsync(createTime);
            var result = Mapper.Map<List<SubscriptionOrder>, List<SubscriptionOrderViewModel>>(orders);
            return Ok(result);
        }

    }
}

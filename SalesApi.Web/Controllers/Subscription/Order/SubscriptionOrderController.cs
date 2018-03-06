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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _subscriptionOrderRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionOrderViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubscriptionOrder")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _subscriptionOrderRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SubscriptionOrderViewModel>(item);
            return Ok(result);
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

            var bonusDateIds = orderVms.SelectMany(x => x.SubscriptionOrderBonusDates)
                .Select(x => x.SubscriptionMonthPromotionBonusDateId).Distinct().ToList();
            var promotionBonusDates = await _subscriptionMonthPromotionBonusDateRepository.All
                .Where(x => bonusDateIds.Contains(x.Id)).ToListAsync();
            if (promotionBonusDates.Any())
            {
                var bonusDates = promotionBonusDates.Select(x => x.Date).ToList();
                _subscriptionOrderService.ValidateOrderBonusDates(bonusDates, Today, Tomorrow, hasDayBeenConfirmed);
            }

            _subscriptionOrderService.ValidateOrderDatesAndModifiedBonusDates(orderVms, Today, Tomorrow, hasDayBeenConfirmed);
            var invalidateDates = await _subscriptionOrderService.ValidateDayCountAsync(milkmanId, orderVms);
            if (invalidateDates.Any())
            {
                return BadRequest(invalidateDates);
            }
            var orders =_subscriptionOrderService.AddSubscriptionOrders(orderVms, UserName);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var result = Mapper.Map<List<SubscriptionOrder>, List<SubscriptionOrderViewModel>>(orders);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubscriptionOrderViewModel subscriptionOrderVm)
        {
            if (subscriptionOrderVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _subscriptionOrderRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(subscriptionOrderVm, dbItem);
            dbItem.SetModification(UserName);
            _subscriptionOrderRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<SubscriptionOrderViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubscriptionOrderViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _subscriptionOrderRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SubscriptionOrderViewModel>(dbItem);
            patchDoc.ApplyTo(toPatchVm, ModelState);

            TryValidateModel(toPatchVm);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(toPatchVm, dbItem);

            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "更新时出错");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _subscriptionOrderRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _subscriptionOrderRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("NotDeleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var items = await _subscriptionOrderRepository.All.Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionOrderViewModel>>(items);
            return Ok(results);
        }
    }
}

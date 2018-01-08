using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Subscription;
using SalesApi.Repositories.Subscription;
using SalesApi.Services.Subscription;
using SalesApi.ViewModels.Subscription;
using SalesApi.Web.Controllers.Bases;
using SharedSettings.Tools;

namespace SalesApi.Web.Controllers.Subscription
{
    [Route("api/sales/[controller]")]
    public class SubscriptionPromotionEventController : SubscriptionController<SubscriptionPromotionEventController>
    {
        private readonly ISubscriptionPromotionEventRepository _subscriptionPromotionEventRepository;
        private readonly ISubscriptionPromotionEventBonusRepository _subscriptionPromotionEventBonusRepository;

        public SubscriptionPromotionEventController(ISubscriptionService<SubscriptionPromotionEventController> subscriptionService,
            ISubscriptionPromotionEventRepository subscriptionPromotionEventRepository,
            ISubscriptionPromotionEventBonusRepository subscriptionPromotionEventBonusRepository) : base(subscriptionService)
        {
            _subscriptionPromotionEventRepository = subscriptionPromotionEventRepository;
            _subscriptionPromotionEventBonusRepository = subscriptionPromotionEventBonusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _subscriptionPromotionEventRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionPromotionEventViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubscriptionPromotionEvent")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _subscriptionPromotionEventRepository
                .GetSingleAsync(x => x.Id == id, x => x.SubscriptionPromotionEventBonuses, x => x.SubscriptionPromotionSeries);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SubscriptionPromotionEventViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionPromotionEventViewModel subscriptionPromotionEventVm)
        {
            if (subscriptionPromotionEventVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await ValidateSubscriptionDay(subscriptionPromotionEventVm.Date);
            var newItem = Mapper.Map<SubscriptionPromotionEvent>(subscriptionPromotionEventVm);
            newItem.SetCreation(UserName);
            _subscriptionPromotionEventRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<SubscriptionPromotionEventViewModel>(newItem);

            return CreatedAtRoute("GetSubscriptionPromotionEvent", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubscriptionPromotionEventViewModel subscriptionPromotionEventVm)
        {
            if (subscriptionPromotionEventVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await ValidateSubscriptionDay(subscriptionPromotionEventVm.Date);
            var dbItem = await _subscriptionPromotionEventRepository.GetSingleAsync(x => x.Id == id, x => x.SubscriptionPromotionEventBonuses);
            if (dbItem == null)
            {
                return NotFound();
            }
            var bonusVms = subscriptionPromotionEventVm.SubscriptionPromotionEventBonuses;
            subscriptionPromotionEventVm.SubscriptionPromotionEventBonuses = null;
            var bonuses = dbItem.SubscriptionPromotionEventBonuses;
            dbItem.SubscriptionPromotionEventBonuses = null;
            Mapper.Map(subscriptionPromotionEventVm, dbItem);
            dbItem.SetModification(UserName);

            var toAddBonusVms = bonusVms.Where(x => x.Id == 0).ToList();
            var toAddBonuses = Mapper.Map<List<SubscriptionPromotionEventBonus>>(toAddBonusVms);
            foreach (var addBonus in toAddBonuses)
            {
                addBonus.SetCreation(UserName);
                _subscriptionPromotionEventBonusRepository.Add(addBonus);
            }
            var bonusVmIds = bonusVms.Where(x => x.Id > 0).Select(x => x.Id).ToList();
            var bonusIds = bonuses.Select(x => x.Id).ToList();
            var toDeleteIds = bonusIds.Except(bonusVmIds);
            var toDeleteBonuses = bonuses.Where(x => toDeleteIds.Contains(x.Id));
            _subscriptionPromotionEventBonusRepository.DeleteRange(toDeleteBonuses);
            var toUpdateIds = bonusIds.Intersect(bonusVmIds);
            var toUpdateBonuses = bonuses.Where(x => toUpdateIds.Contains(x.Id)).ToList();
            foreach (var bonus in toUpdateBonuses)
            {
                var bonusVm = bonusVms.SingleOrDefault(x => x.Id == bonus.Id);
                Mapper.Map(bonusVm, bonus);
                bonus.SetModification(UserName);
                _subscriptionPromotionEventBonusRepository.Update(bonus);
            }
            dbItem.SubscriptionPromotionEventBonuses = toAddBonuses.Concat(toUpdateBonuses).ToList();
            _subscriptionPromotionEventRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubscriptionPromotionEventViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _subscriptionPromotionEventRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SubscriptionPromotionEventViewModel>(dbItem);
            patchDoc.ApplyTo(toPatchVm, ModelState);

            TryValidateModel(toPatchVm);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await ValidateSubscriptionDay(toPatchVm.Date);

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
            var model = await _subscriptionPromotionEventRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            await ValidateSubscriptionDay(model.Date);
            _subscriptionPromotionEventRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [NonAction]
        private async Task ValidateSubscriptionDay(DateTime date)
        {
            var latestSubscriptionDay = await SubscriptionDayRepository.All.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (latestSubscriptionDay != null)
            {
                var latestSubscriptionDate = DateTime.ParseExact(latestSubscriptionDay.Date, DateTools.OrderDateFormat,
                    CultureInfo.InvariantCulture);
                if (date <= latestSubscriptionDate)
                {
                    throw new Exception("该日期已经初始化或不是最近日期, 无法添加/修改/删除买赠活动事件");
                }
            }
        }

        [HttpGet]
        [Route("ByRange")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByRange(DateTime start, DateTime end)
        {
            var items = await _subscriptionPromotionEventRepository
                .AllIncluding(x => x.SubscriptionPromotionEventBonuses)
                .Where(x => x.Date >= start && x.Date <= end).ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionPromotionEventForFullCalendarViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("ByDate/{date?}")]
        public async Task<IActionResult> GetByDate(DateTime? date = null)
        {
            var theDate = date ?? Tomorrow;
            var items = await _subscriptionPromotionEventRepository.AllIncluding(x => x.SubscriptionPromotionEventBonuses).Where(x => x.Date == theDate).ToListAsync();
            var vms = Mapper.Map<IEnumerable<SubscriptionPromotionEventViewModel>>(items);
            return Ok(vms);
        }
    }
}

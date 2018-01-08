using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
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
    public class SubscriptionPromotionSeriesController : SubscriptionController<SubscriptionPromotionSeriesController>
    {
        private readonly ISubscriptionPromotionSeriesRepository _subscriptionPromotionSeriesRepository;
        private readonly ISubscriptionPromotionEventRepository _subscriptionPromotionEventRepository;
        private readonly ISubscriptionPromotionSeriesBonusRepository _subscriptionPromotionSeriesBonusRepository;
        private readonly ISubscriptionPromotionEventBonusRepository _subscriptionPromotionEventBonusRepository;

        public SubscriptionPromotionSeriesController(ISubscriptionService<SubscriptionPromotionSeriesController> subscriptionService,
            ISubscriptionPromotionSeriesRepository subscriptionPromotionSeriesRepository,
            ISubscriptionPromotionEventRepository subscriptionPromotionEventRepository,
            ISubscriptionPromotionSeriesBonusRepository subscriptionPromotionSeriesBonusRepository,
            ISubscriptionPromotionEventBonusRepository subscriptionPromotionEventBonusRepository) : base(subscriptionService)
        {
            _subscriptionPromotionSeriesRepository = subscriptionPromotionSeriesRepository;
            _subscriptionPromotionEventRepository = subscriptionPromotionEventRepository;
            _subscriptionPromotionSeriesBonusRepository = subscriptionPromotionSeriesBonusRepository;
            _subscriptionPromotionEventBonusRepository = subscriptionPromotionEventBonusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _subscriptionPromotionSeriesRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionPromotionSeriesViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubscriptionPromotionSeries")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _subscriptionPromotionSeriesRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SubscriptionPromotionSeriesViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionPromotionSeriesAddViewModel subscriptionPromotionSeriesVm)
        {
            if (subscriptionPromotionSeriesVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await ValidateSubscriptionDay(subscriptionPromotionSeriesVm.StartDate);
            var newItem = Mapper.Map<SubscriptionPromotionSeries>(subscriptionPromotionSeriesVm);
            newItem.SetCreation(UserName);
            foreach (var newItemSubscriptionPromotionSeriesBonus in newItem.SubscriptionPromotionSeriesBonuses)
            {
                newItemSubscriptionPromotionSeriesBonus.SetCreation(UserName);
            }
            var events = _subscriptionPromotionEventRepository.GenerateEvents(newItem).ToList();
            TryValidateModel(events);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            newItem.SubscriptionPromotionEvents = events;
            _subscriptionPromotionSeriesRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<SubscriptionPromotionSeriesViewModel>(newItem);

            return CreatedAtRoute("GetSubscriptionPromotionSeries", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubscriptionPromotionSeriesEditViewModel subscriptionPromotionSeriesVm)
        {
            if (subscriptionPromotionSeriesVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _subscriptionPromotionSeriesRepository.GetSingleAsync(x => x.Id == id, x => x.SubscriptionPromotionSeriesBonuses);
            if (dbItem == null)
            {
                return NotFound();
            }
            await ValidateSubscriptionDay(subscriptionPromotionSeriesVm.StartDate);
            var bonusVms = subscriptionPromotionSeriesVm.SubscriptionPromotionSeriesBonuses;
            subscriptionPromotionSeriesVm.SubscriptionPromotionSeriesBonuses = null;
            var bonuses = dbItem.SubscriptionPromotionSeriesBonuses;
            dbItem.SubscriptionPromotionSeriesBonuses = null;
            Mapper.Map(subscriptionPromotionSeriesVm, dbItem);
            dbItem.SetModification(UserName);

            var toAddVms = bonusVms.Where(x => x.Id == 0).ToList();
            var toAdd = Mapper.Map<List<SubscriptionPromotionSeriesBonus>>(toAddVms);
            foreach (var bonus in toAdd)
            {
                bonus.SetCreation(UserName);
            }
            _subscriptionPromotionSeriesBonusRepository.AddRange(toAdd);

            var vmIds = bonusVms.Where(x => x.Id != 0).Select(x => x.Id).ToList();
            var dbIds = bonuses.Select(x => x.Id).ToList();
            var toDeleteIds = dbIds.Except(vmIds).ToList();
            var toDelete = bonuses.Where(x => toDeleteIds.Contains(x.Id)).ToList();
            _subscriptionPromotionSeriesBonusRepository.DeleteRange(toDelete);

            var toUpdateIds = vmIds.Intersect(dbIds).ToList();
            var toUpdate = bonuses.Where(x => toUpdateIds.Contains(x.Id)).ToList();
            foreach (var bonus in toUpdate)
            {
                var vm = bonusVms.SingleOrDefault(x => x.Id == bonus.Id);
                if (vm != null)
                {
                    Mapper.Map(vm, bonus);
                    bonus.SetModification(UserName);
                    _subscriptionPromotionSeriesBonusRepository.Update(bonus);
                }
            }

            dbItem.SubscriptionPromotionSeriesBonuses = toUpdate.Concat(toAdd).ToList();

            var toDeleteEvents = await _subscriptionPromotionEventRepository
                .AllIncluding(x => x.SubscriptionPromotionEventBonuses)
                .Where(x => x.SubscriptionPromotionSeriesId == id).ToListAsync();
            var toDeleteEventBonuses = toDeleteEvents.SelectMany(x => x.SubscriptionPromotionEventBonuses).ToList();
            _subscriptionPromotionEventBonusRepository.DeleteRange(toDeleteEventBonuses);
            _subscriptionPromotionEventRepository.DeleteRange(toDeleteEvents);

            var events = _subscriptionPromotionEventRepository.GenerateEvents(dbItem).ToList();
            TryValidateModel(events);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _subscriptionPromotionEventRepository.AddRange(events);

            _subscriptionPromotionSeriesRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubscriptionPromotionSeriesViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _subscriptionPromotionSeriesRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SubscriptionPromotionSeriesViewModel>(dbItem);
            patchDoc.ApplyTo(toPatchVm, ModelState);

            TryValidateModel(toPatchVm);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await ValidateSubscriptionDay(toPatchVm.StartDate);
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
            var model = await _subscriptionPromotionSeriesRepository
                .GetSingleAsync(x => x.Id == id, x => x.SubscriptionPromotionSeriesBonuses, x => x.SubscriptionPromotionEvents);
            if (model == null)
            {
                return NotFound();
            }
            var eventBonuses = await _subscriptionPromotionEventBonusRepository.All
                .Where(x => x.SubscriptionPromotionEvent.SubscriptionPromotionSeriesId == id).ToListAsync();
            _subscriptionPromotionSeriesBonusRepository.DeleteRange(model.SubscriptionPromotionSeriesBonuses);
            _subscriptionPromotionEventBonusRepository.DeleteRange(eventBonuses);
            _subscriptionPromotionEventRepository.DeleteRange(model.SubscriptionPromotionEvents);
            _subscriptionPromotionSeriesRepository.Delete(model);
            await ValidateSubscriptionDay(model.StartDate);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [NonAction]
        private async Task ValidateSubscriptionDay(DateTime startDate)
        {
            var latestSubscriptionDay = await SubscriptionDayRepository.All.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (latestSubscriptionDay != null)
            {
                var latestSubscriptionDate = DateTime.ParseExact(latestSubscriptionDay.Date, DateTools.OrderDateFormat,
                    CultureInfo.InvariantCulture);
                if (startDate <= latestSubscriptionDate)
                {
                    throw new Exception("区间内有日期已经初始化, 无法添加/修改/删除买赠活动序列");
                }
            }
        }
    }
}

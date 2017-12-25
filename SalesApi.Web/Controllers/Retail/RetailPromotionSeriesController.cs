using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Retail;
using SalesApi.Repositories.Retail;
using SalesApi.Services.Retail;
using SalesApi.ViewModels.Retail;
using SalesApi.Web.Controllers.Bases;
using SharedSettings.Tools;

namespace SalesApi.Web.Controllers.Retail
{
    [Route("api/sales/[controller]")]
    public class RetailPromotionSeriesController : RetailController<RetailPromotionSeriesController>
    {
        private readonly IRetailPromotionSeriesRepository _retailPromotionSeriesRepository;
        private readonly IRetailPromotionEventRepository _retailPromotionEventRepository;
        private readonly IRetailPromotionSeriesBonusRepository _retailPromotionSeriesBonusRepository;
        private readonly IRetailPromotionEventBonusRepository _retailPromotionEventBonusRepository;

        public RetailPromotionSeriesController(IRetailService<RetailPromotionSeriesController> retailService,
            IRetailPromotionSeriesRepository retailPromotionSeriesRepository,
            IRetailPromotionEventRepository retailPromotionEventRepository,
            IRetailPromotionSeriesBonusRepository retailPromotionSeriesBonusRepository,
            IRetailPromotionEventBonusRepository retailPromotionEventBonusRepository) : base(retailService)
        {
            _retailPromotionSeriesRepository = retailPromotionSeriesRepository;
            _retailPromotionEventRepository = retailPromotionEventRepository;
            _retailPromotionSeriesBonusRepository = retailPromotionSeriesBonusRepository;
            _retailPromotionEventBonusRepository = retailPromotionEventBonusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _retailPromotionSeriesRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<RetailPromotionSeriesViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetRetailPromotionSeries")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _retailPromotionSeriesRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<RetailPromotionSeriesViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RetailPromotionSeriesAddViewModel retailPromotionSeriesVm)
        {
            if (retailPromotionSeriesVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await ValidateRetailDay(retailPromotionSeriesVm.StartDate);
            var newItem = Mapper.Map<RetailPromotionSeries>(retailPromotionSeriesVm);
            newItem.SetCreation(UserName);
            foreach (var newItemRetailPromotionSeriesBonus in newItem.RetailPromotionSeriesBonuses)
            {
                newItemRetailPromotionSeriesBonus.SetCreation(UserName);
            }
            var events = _retailPromotionEventRepository.GenerateEvents(newItem).ToList();
            TryValidateModel(events);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            newItem.RetailPromotionEvents = events;
            _retailPromotionSeriesRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<RetailPromotionSeriesViewModel>(newItem);

            return CreatedAtRoute("GetRetailPromotionSeries", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RetailPromotionSeriesEditViewModel retailPromotionSeriesVm)
        {
            if (retailPromotionSeriesVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _retailPromotionSeriesRepository.GetSingleAsync(x => x.Id == id, x => x.RetailPromotionSeriesBonuses);
            if (dbItem == null)
            {
                return NotFound();
            }
            await ValidateRetailDay(retailPromotionSeriesVm.StartDate);
            var bonusVms = retailPromotionSeriesVm.RetailPromotionSeriesBonuses;
            retailPromotionSeriesVm.RetailPromotionSeriesBonuses = null;
            var bonuses = dbItem.RetailPromotionSeriesBonuses;
            dbItem.RetailPromotionSeriesBonuses = null;
            Mapper.Map(retailPromotionSeriesVm, dbItem);
            dbItem.SetModification(UserName);

            var toAddVms = bonusVms.Where(x => x.Id == 0).ToList();
            var toAdd = Mapper.Map<List<RetailPromotionSeriesBonus>>(toAddVms);
            foreach (var bonus in toAdd)
            {
                bonus.SetCreation(UserName);
            }
            _retailPromotionSeriesBonusRepository.AddRange(toAdd);

            var vmIds = bonusVms.Where(x => x.Id != 0).Select(x => x.Id).ToList();
            var dbIds = bonuses.Select(x => x.Id).ToList();
            var toDeleteIds = dbIds.Except(vmIds).ToList();
            var toDelete = bonuses.Where(x => toDeleteIds.Contains(x.Id)).ToList();
            _retailPromotionSeriesBonusRepository.DeleteRange(toDelete);

            var toUpdateIds = vmIds.Intersect(dbIds).ToList();
            var toUpdate = bonuses.Where(x => toUpdateIds.Contains(x.Id)).ToList();
            foreach (var bonus in toUpdate)
            {
                var vm = bonusVms.SingleOrDefault(x => x.Id == bonus.Id);
                if (vm != null)
                {
                    Mapper.Map(vm, bonus);
                    bonus.SetModification(UserName);
                    _retailPromotionSeriesBonusRepository.Update(bonus);
                }
            }

            dbItem.RetailPromotionSeriesBonuses = toUpdate.Concat(toAdd).ToList();

            var toDeleteEvents = await _retailPromotionEventRepository
                .AllIncluding(x => x.RetailPromotionEventBonuses)
                .Where(x => x.RetailPromotionSeriesId == id).ToListAsync();
            var toDeleteEventBonuses = toDeleteEvents.SelectMany(x => x.RetailPromotionEventBonuses).ToList();
            _retailPromotionEventBonusRepository.DeleteRange(toDeleteEventBonuses);
            _retailPromotionEventRepository.DeleteRange(toDeleteEvents);

            var events = _retailPromotionEventRepository.GenerateEvents(dbItem).ToList();
            TryValidateModel(events);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _retailPromotionEventRepository.AddRange(events);

            _retailPromotionSeriesRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<RetailPromotionSeriesViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _retailPromotionSeriesRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<RetailPromotionSeriesViewModel>(dbItem);
            patchDoc.ApplyTo(toPatchVm, ModelState);

            TryValidateModel(toPatchVm);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await ValidateRetailDay(toPatchVm.StartDate);
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
            var model = await _retailPromotionSeriesRepository
                .GetSingleAsync(x => x.Id == id, x => x.RetailPromotionSeriesBonuses, x => x.RetailPromotionEvents);
            if (model == null)
            {
                return NotFound();
            }
            var eventBonuses = await _retailPromotionEventBonusRepository.All
                .Where(x => x.RetailPromotionEvent.RetailPromotionSeriesId == id).ToListAsync();
            _retailPromotionSeriesBonusRepository.DeleteRange(model.RetailPromotionSeriesBonuses);
            _retailPromotionEventBonusRepository.DeleteRange(eventBonuses);
            _retailPromotionEventRepository.DeleteRange(model.RetailPromotionEvents);
            _retailPromotionSeriesRepository.Delete(model);
            await ValidateRetailDay(model.StartDate);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [NonAction]
        private async Task ValidateRetailDay(DateTime startDate)
        {
            var latestRetailDay = await RetailDayRepository.All.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (latestRetailDay != null)
            {
                var latestRetailDate = DateTime.ParseExact(latestRetailDay.Date, DateTools.OrderDateFormat,
                    CultureInfo.InvariantCulture);
                if (startDate <= latestRetailDate)
                {
                    throw new Exception("区间内有日期已经初始化, 无法添加/修改/删除买赠活动序列");
                }
            }
        }
    }
}

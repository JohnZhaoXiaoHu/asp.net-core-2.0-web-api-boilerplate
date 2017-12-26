using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.County;
using SalesApi.Repositories.County;
using SalesApi.Services.County;
using SalesApi.ViewModels.County;
using SalesApi.Web.Controllers.Bases;
using SharedSettings.Tools;

namespace SalesApi.Web.Controllers.County
{
    [Route("api/sales/[controller]")]
    public class CountyPromotionSeriesController : CountyController<CountyPromotionSeriesController>
    {
        private readonly ICountyPromotionSeriesRepository _countyPromotionSeriesRepository;
        private readonly ICountyPromotionEventRepository _countyPromotionEventRepository;
        private readonly ICountyPromotionSeriesBonusRepository _countyPromotionSeriesBonusRepository;
        private readonly ICountyPromotionEventBonusRepository _countyPromotionEventBonusRepository;

        public CountyPromotionSeriesController(ICountyService<CountyPromotionSeriesController> countyService,
            ICountyPromotionSeriesRepository countyPromotionSeriesRepository,
            ICountyPromotionEventRepository countyPromotionEventRepository,
            ICountyPromotionSeriesBonusRepository countyPromotionSeriesBonusRepository,
            ICountyPromotionEventBonusRepository countyPromotionEventBonusRepository) : base(countyService)
        {
            _countyPromotionSeriesRepository = countyPromotionSeriesRepository;
            _countyPromotionEventRepository = countyPromotionEventRepository;
            _countyPromotionSeriesBonusRepository = countyPromotionSeriesBonusRepository;
            _countyPromotionEventBonusRepository = countyPromotionEventBonusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _countyPromotionSeriesRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyPromotionSeriesViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetCountyPromotionSeries")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _countyPromotionSeriesRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<CountyPromotionSeriesViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CountyPromotionSeriesAddViewModel countyPromotionSeriesVm)
        {
            if (countyPromotionSeriesVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await ValidateCountyDay(countyPromotionSeriesVm.StartDate);
            var newItem = Mapper.Map<CountyPromotionSeries>(countyPromotionSeriesVm);
            newItem.SetCreation(UserName);
            foreach (var newItemCountyPromotionSeriesBonus in newItem.CountyPromotionSeriesBonuses)
            {
                newItemCountyPromotionSeriesBonus.SetCreation(UserName);
            }
            var events = _countyPromotionEventRepository.GenerateEvents(newItem).ToList();
            TryValidateModel(events);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            newItem.CountyPromotionEvents = events;
            _countyPromotionSeriesRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<CountyPromotionSeriesViewModel>(newItem);

            return CreatedAtRoute("GetCountyPromotionSeries", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CountyPromotionSeriesEditViewModel countyPromotionSeriesVm)
        {
            if (countyPromotionSeriesVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _countyPromotionSeriesRepository.GetSingleAsync(x => x.Id == id, x => x.CountyPromotionSeriesBonuses);
            if (dbItem == null)
            {
                return NotFound();
            }
            await ValidateCountyDay(countyPromotionSeriesVm.StartDate);
            var bonusVms = countyPromotionSeriesVm.CountyPromotionSeriesBonuses;
            countyPromotionSeriesVm.CountyPromotionSeriesBonuses = null;
            var bonuses = dbItem.CountyPromotionSeriesBonuses;
            dbItem.CountyPromotionSeriesBonuses = null;
            Mapper.Map(countyPromotionSeriesVm, dbItem);
            dbItem.SetModification(UserName);

            var toAddVms = bonusVms.Where(x => x.Id == 0).ToList();
            var toAdd = Mapper.Map<List<CountyPromotionSeriesBonus>>(toAddVms);
            foreach (var bonus in toAdd)
            {
                bonus.SetCreation(UserName);
            }
            _countyPromotionSeriesBonusRepository.AddRange(toAdd);

            var vmIds = bonusVms.Where(x => x.Id != 0).Select(x => x.Id).ToList();
            var dbIds = bonuses.Select(x => x.Id).ToList();
            var toDeleteIds = dbIds.Except(vmIds).ToList();
            var toDelete = bonuses.Where(x => toDeleteIds.Contains(x.Id)).ToList();
            _countyPromotionSeriesBonusRepository.DeleteRange(toDelete);

            var toUpdateIds = vmIds.Intersect(dbIds).ToList();
            var toUpdate = bonuses.Where(x => toUpdateIds.Contains(x.Id)).ToList();
            foreach (var bonus in toUpdate)
            {
                var vm = bonusVms.SingleOrDefault(x => x.Id == bonus.Id);
                if (vm != null)
                {
                    Mapper.Map(vm, bonus);
                    bonus.SetModification(UserName);
                    _countyPromotionSeriesBonusRepository.Update(bonus);
                }
            }

            dbItem.CountyPromotionSeriesBonuses = toUpdate.Concat(toAdd).ToList();

            var toDeleteEvents = await _countyPromotionEventRepository
                .AllIncluding(x => x.CountyPromotionEventBonuses)
                .Where(x => x.CountyPromotionSeriesId == id).ToListAsync();
            var toDeleteEventBonuses = toDeleteEvents.SelectMany(x => x.CountyPromotionEventBonuses).ToList();
            _countyPromotionEventBonusRepository.DeleteRange(toDeleteEventBonuses);
            _countyPromotionEventRepository.DeleteRange(toDeleteEvents);

            var events = _countyPromotionEventRepository.GenerateEvents(dbItem).ToList();
            TryValidateModel(events);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _countyPromotionEventRepository.AddRange(events);

            _countyPromotionSeriesRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CountyPromotionSeriesViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _countyPromotionSeriesRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<CountyPromotionSeriesViewModel>(dbItem);
            patchDoc.ApplyTo(toPatchVm, ModelState);

            TryValidateModel(toPatchVm);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await ValidateCountyDay(toPatchVm.StartDate);
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
            var model = await _countyPromotionSeriesRepository
                .GetSingleAsync(x => x.Id == id, x => x.CountyPromotionSeriesBonuses, x => x.CountyPromotionEvents);
            if (model == null)
            {
                return NotFound();
            }
            var eventBonuses = await _countyPromotionEventBonusRepository.All
                .Where(x => x.CountyPromotionEvent.CountyPromotionSeriesId == id).ToListAsync();
            _countyPromotionSeriesBonusRepository.DeleteRange(model.CountyPromotionSeriesBonuses);
            _countyPromotionEventBonusRepository.DeleteRange(eventBonuses);
            _countyPromotionEventRepository.DeleteRange(model.CountyPromotionEvents);
            _countyPromotionSeriesRepository.Delete(model);
            await ValidateCountyDay(model.StartDate);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [NonAction]
        private async Task ValidateCountyDay(DateTime startDate)
        {
            var latestCountyDay = await CountyDayRepository.All.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (latestCountyDay != null)
            {
                var latestCountyDate = DateTime.ParseExact(latestCountyDay.Date, DateTools.OrderDateFormat,
                    CultureInfo.InvariantCulture);
                if (startDate <= latestCountyDate)
                {
                    throw new Exception("区间内有日期已经初始化, 无法添加/修改/删除买赠活动序列");
                }
            }
        }
    }
}

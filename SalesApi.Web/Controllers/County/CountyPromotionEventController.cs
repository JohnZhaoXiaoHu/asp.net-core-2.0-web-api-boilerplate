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
using SalesApi.Models.County;
using SalesApi.Repositories.County;
using SalesApi.Services.County;
using SalesApi.ViewModels.County;
using SalesApi.Web.Controllers.Bases;
using SharedSettings.Tools;

namespace SalesApi.Web.Controllers.County
{
    [Route("api/sales/[controller]")]
    public class CountyPromotionEventController : CountyController<CountyPromotionEventController>
    {
        private readonly ICountyPromotionEventRepository _countyPromotionEventRepository;
        private readonly ICountyPromotionEventBonusRepository _countyPromotionEventBonusRepository;

        public CountyPromotionEventController(ICountyService<CountyPromotionEventController> countyService,
            ICountyPromotionEventRepository countyPromotionEventRepository,
            ICountyPromotionEventBonusRepository countyPromotionEventBonusRepository) : base(countyService)
        {
            _countyPromotionEventRepository = countyPromotionEventRepository;
            _countyPromotionEventBonusRepository = countyPromotionEventBonusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _countyPromotionEventRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyPromotionEventViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetCountyPromotionEvent")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _countyPromotionEventRepository
                .GetSingleAsync(x => x.Id == id, x => x.CountyPromotionEventBonuses, x => x.CountyPromotionSeries);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<CountyPromotionEventViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CountyPromotionEventViewModel countyPromotionEventVm)
        {
            if (countyPromotionEventVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await ValidateCountyDay(countyPromotionEventVm.Date);
            var newItem = Mapper.Map<CountyPromotionEvent>(countyPromotionEventVm);
            newItem.SetCreation(UserName);
            _countyPromotionEventRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<CountyPromotionEventViewModel>(newItem);

            return CreatedAtRoute("GetCountyPromotionEvent", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CountyPromotionEventViewModel countyPromotionEventVm)
        {
            if (countyPromotionEventVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await ValidateCountyDay(countyPromotionEventVm.Date);
            var dbItem = await _countyPromotionEventRepository.GetSingleAsync(x => x.Id == id, x => x.CountyPromotionEventBonuses);
            if (dbItem == null)
            {
                return NotFound();
            }
            var bonusVms = countyPromotionEventVm.CountyPromotionEventBonuses;
            countyPromotionEventVm.CountyPromotionEventBonuses = null;
            var bonuses = dbItem.CountyPromotionEventBonuses;
            dbItem.CountyPromotionEventBonuses = null;
            Mapper.Map(countyPromotionEventVm, dbItem);
            dbItem.SetModification(UserName);

            var toAddBonusVms = bonusVms.Where(x => x.Id == 0).ToList();
            var toAddBonuses = Mapper.Map<List<CountyPromotionEventBonus>>(toAddBonusVms);
            foreach (var addBonus in toAddBonuses)
            {
                addBonus.SetCreation(UserName);
                _countyPromotionEventBonusRepository.Add(addBonus);
            }
            var bonusVmIds = bonusVms.Where(x => x.Id > 0).Select(x => x.Id).ToList();
            var bonusIds = bonuses.Select(x => x.Id).ToList();
            var toDeleteIds = bonusIds.Except(bonusVmIds);
            var toDeleteBonuses = bonuses.Where(x => toDeleteIds.Contains(x.Id));
            _countyPromotionEventBonusRepository.DeleteRange(toDeleteBonuses);
            var toUpdateIds = bonusIds.Intersect(bonusVmIds);
            var toUpdateBonuses = bonuses.Where(x => toUpdateIds.Contains(x.Id)).ToList();
            foreach (var bonus in toUpdateBonuses)
            {
                var bonusVm = bonusVms.SingleOrDefault(x => x.Id == bonus.Id);
                Mapper.Map(bonusVm, bonus);
                bonus.SetModification(UserName);
                _countyPromotionEventBonusRepository.Update(bonus);
            }
            dbItem.CountyPromotionEventBonuses = toAddBonuses.Concat(toUpdateBonuses).ToList();
            _countyPromotionEventRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CountyPromotionEventViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _countyPromotionEventRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<CountyPromotionEventViewModel>(dbItem);
            patchDoc.ApplyTo(toPatchVm, ModelState);

            TryValidateModel(toPatchVm);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await ValidateCountyDay(toPatchVm.Date);

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
            var model = await _countyPromotionEventRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            await ValidateCountyDay(model.Date);
            _countyPromotionEventRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [NonAction]
        private async Task ValidateCountyDay(DateTime date)
        {
            var latestCountyDay = await CountyDayRepository.All.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (latestCountyDay != null)
            {
                var latestCountyDate = DateTime.ParseExact(latestCountyDay.Date, DateTools.OrderDateFormat,
                    CultureInfo.InvariantCulture);
                if (date <= latestCountyDate)
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
            var items = await _countyPromotionEventRepository
                .AllIncluding(x => x.CountyPromotionEventBonuses)
                .Where(x => x.Date >= start && x.Date <= end).ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyPromotionEventForFullCalendarViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("ByDate/{date?}")]
        public async Task<IActionResult> GetByDate(DateTime? date = null)
        {
            var theDate = date ?? Tomorrow;
            var items = await _countyPromotionEventRepository.AllIncluding(x => x.CountyPromotionEventBonuses).Where(x => x.Date == theDate).ToListAsync();
            var vms = Mapper.Map<IEnumerable<CountyPromotionEventViewModel>>(items);
            return Ok(vms);
        }
    }
}

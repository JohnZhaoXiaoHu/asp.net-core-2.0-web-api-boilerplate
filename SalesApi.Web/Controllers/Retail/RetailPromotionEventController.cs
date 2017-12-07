using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Retail;
using SalesApi.Repositories.Retail;
using SalesApi.Services.Retail;
using SalesApi.ViewModels.Retail;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Retail
{
    [Route("api/sales/[controller]")]
    public class RetailPromotionEventController : RetailController<RetailPromotionEventController>
    {
        private readonly IRetailPromotionEventRepository _retailPromotionEventRepository;
        private readonly IRetailPromotionEventBonusRepository _retailPromotionEventBonusRepository;

        public RetailPromotionEventController(IRetailService<RetailPromotionEventController> retailService,
            IRetailPromotionEventRepository retailPromotionEventRepository,
            IRetailPromotionEventBonusRepository retailPromotionEventBonusRepository) : base(retailService)
        {
            _retailPromotionEventRepository = retailPromotionEventRepository;
            _retailPromotionEventBonusRepository = retailPromotionEventBonusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _retailPromotionEventRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<RetailPromotionEventViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetRetailPromotionEvent")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _retailPromotionEventRepository
                .GetSingleAsync(x => x.Id == id, x => x.RetailPromotionEventBonuses, x => x.RetailPromotionSeries);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<RetailPromotionEventViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RetailPromotionEventViewModel retailPromotionEventVm)
        {
            if (retailPromotionEventVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<RetailPromotionEvent>(retailPromotionEventVm);
            newItem.SetCreation(UserName);
            _retailPromotionEventRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<RetailPromotionEventViewModel>(newItem);

            return CreatedAtRoute("GetRetailPromotionEvent", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RetailPromotionEventViewModel retailPromotionEventVm)
        {
            if (retailPromotionEventVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _retailPromotionEventRepository.GetSingleAsync(x => x.Id == id, x => x.RetailPromotionEventBonuses);
            if (dbItem == null)
            {
                return NotFound();
            }
            var bonusVms = retailPromotionEventVm.RetailPromotionEventBonuses;
            retailPromotionEventVm.RetailPromotionEventBonuses = null;
            var bonuses = dbItem.RetailPromotionEventBonuses;
            dbItem.RetailPromotionEventBonuses = null;
            Mapper.Map(retailPromotionEventVm, dbItem);
            dbItem.SetModification(UserName);

            var toAddBonusVms = bonusVms.Where(x => x.Id == 0).ToList();
            var toAddBonuses = Mapper.Map<List<RetailPromotionEventBonus>>(toAddBonusVms);
            foreach (var addBonus in toAddBonuses)
            {
                addBonus.SetCreation(UserName);
                _retailPromotionEventBonusRepository.Add(addBonus);
            }
            var bonusVmIds = bonusVms.Where(x => x.Id > 0).Select(x => x.Id).ToList();
            var bonusIds = bonuses.Select(x => x.Id).ToList();
            var toDeleteIds = bonusIds.Except(bonusVmIds);
            var toDeleteBonuses = bonuses.Where(x => toDeleteIds.Contains(x.Id));
            _retailPromotionEventBonusRepository.DeleteRange(toDeleteBonuses);
            var toUpdateIds = bonusIds.Intersect(bonusVmIds);
            var toUpdateBonuses = bonuses.Where(x => toUpdateIds.Contains(x.Id)).ToList();
            foreach (var bonus in toUpdateBonuses)
            {
                var bonusVm = bonusVms.SingleOrDefault(x => x.Id == bonus.Id);
                Mapper.Map(bonusVm, bonus);
                bonus.SetModification(UserName);
                _retailPromotionEventBonusRepository.Update(bonus);
            }
            dbItem.RetailPromotionEventBonuses = toAddBonuses.Concat(toUpdateBonuses).ToList();
            _retailPromotionEventRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<RetailPromotionEventViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _retailPromotionEventRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<RetailPromotionEventViewModel>(dbItem);
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
            var model = await _retailPromotionEventRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _retailPromotionEventRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("ByRange")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByRange(DateTime start, DateTime end)
        {
            var items = await _retailPromotionEventRepository
                .AllIncluding(x => x.RetailPromotionEventBonuses)
                .Where(x => x.Date >= start && x.Date <= end).ToListAsync();
            var results = Mapper.Map<IEnumerable<RetailPromotionEventForFullCalendarViewModel>>(items);
            return Ok(results);
        }

    }
}

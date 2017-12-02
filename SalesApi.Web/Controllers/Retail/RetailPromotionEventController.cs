using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Retail;
using SalesApi.Repositories.Retail;
using SalesApi.ViewModels.Retail;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Retail
{
    [Route("api/sales/[controller]")]
    public class RetailPromotionEventController : SalesController<RetailPromotionEventController>
    {
        private readonly IRetailPromotionEventRepository _retailPromotionEventRepository;
        public RetailPromotionEventController(ICoreService<RetailPromotionEventController> coreService,
            IRetailPromotionEventRepository retailPromotionEventRepository) : base(coreService)
        {
            _retailPromotionEventRepository = retailPromotionEventRepository;
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
            var item = await _retailPromotionEventRepository.GetSingleAsync(id);
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
            var dbItem = await _retailPromotionEventRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(retailPromotionEventVm, dbItem);
            dbItem.SetModification(UserName);
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

using System;
using System.Collections.Generic;
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

namespace SalesApi.Web.Controllers.Retail
{
    [Route("api/sales/[controller]")]
    public class RetailPromotionGiftOrderController : RetailController<RetailPromotionGiftOrderController>
    {
        private readonly IRetailPromotionGiftOrderRepository _retailPromotionGiftOrderRepository;

        public RetailPromotionGiftOrderController(IRetailService<RetailPromotionGiftOrderController> retailService,
            IRetailPromotionGiftOrderRepository retailPromotionGiftOrderRepository) : base(retailService)
        {
            _retailPromotionGiftOrderRepository = retailPromotionGiftOrderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _retailPromotionGiftOrderRepository.AllIncluding(x => x.RetailOrder, x => x.RetailPromotionEventBonus)
                .ToListAsync();
            var results = Mapper.Map<IEnumerable<RetailPromotionGiftOrderViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetRetailPromotionGiftOrder")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _retailPromotionGiftOrderRepository.GetSingleAsync(x => x.Id == id, x => x.RetailOrder, x => x.RetailPromotionEventBonus);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<RetailPromotionGiftOrderViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RetailPromotionGiftOrderViewModel retailPromotionGiftOrderVm)
        {
            if (retailPromotionGiftOrderVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<RetailPromotionGiftOrder>(retailPromotionGiftOrderVm);
            newItem.SetCreation(UserName);
            _retailPromotionGiftOrderRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<RetailPromotionGiftOrderViewModel>(newItem);

            return CreatedAtRoute("GetRetailPromotionGiftOrder", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RetailPromotionGiftOrderViewModel retailPromotionGiftOrderVm)
        {
            if (retailPromotionGiftOrderVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _retailPromotionGiftOrderRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(retailPromotionGiftOrderVm, dbItem);
            dbItem.SetModification(UserName);
            _retailPromotionGiftOrderRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<RetailPromotionGiftOrderViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<RetailPromotionGiftOrderViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _retailPromotionGiftOrderRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<RetailPromotionGiftOrderViewModel>(dbItem);
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
            var model = await _retailPromotionGiftOrderRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _retailPromotionGiftOrderRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("ByDateAndRetailer/{date}/{retailerId}")]
        public async Task<IActionResult> GetByDateAndRetailer(DateTime date, int retailerId)
        {
            var items = await _retailPromotionGiftOrderRepository.AllIncluding(x => x.RetailOrder, x => x.RetailPromotionEventBonus)
                .Where(x => x.RetailPromotionEventBonus.RetailPromotionEvent.Date == date && x.RetailOrder.RetailerId == retailerId)
                .ToListAsync();
            var results = Mapper.Map<IEnumerable<RetailPromotionGiftOrderViewModel>>(items);
            return Ok(results);
        }
    }
}

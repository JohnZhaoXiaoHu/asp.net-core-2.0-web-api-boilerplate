using System;
using System.Collections.Generic;
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

namespace SalesApi.Web.Controllers.County
{
    [Route("api/sales/[controller]")]
    public class CountyPromotionGiftOrderController : CountyController<CountyPromotionGiftOrderController>
    {
        private readonly ICountyPromotionGiftOrderRepository _countyPromotionGiftOrderRepository;

        public CountyPromotionGiftOrderController(ICountyService<CountyPromotionGiftOrderController> countyService,
            ICountyPromotionGiftOrderRepository countyPromotionGiftOrderRepository) : base(countyService)
        {
            _countyPromotionGiftOrderRepository = countyPromotionGiftOrderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _countyPromotionGiftOrderRepository.AllIncluding(x => x.CountyOrder, x => x.CountyPromotionEventBonus)
                .ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyPromotionGiftOrderViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetCountyPromotionGiftOrder")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _countyPromotionGiftOrderRepository.GetSingleAsync(x => x.Id == id, x => x.CountyOrder, x => x.CountyPromotionEventBonus);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<CountyPromotionGiftOrderViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CountyPromotionGiftOrderViewModel countyPromotionGiftOrderVm)
        {
            if (countyPromotionGiftOrderVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<CountyPromotionGiftOrder>(countyPromotionGiftOrderVm);
            newItem.SetCreation(UserName);
            _countyPromotionGiftOrderRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<CountyPromotionGiftOrderViewModel>(newItem);

            return CreatedAtRoute("GetCountyPromotionGiftOrder", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CountyPromotionGiftOrderViewModel countyPromotionGiftOrderVm)
        {
            if (countyPromotionGiftOrderVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _countyPromotionGiftOrderRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(countyPromotionGiftOrderVm, dbItem);
            dbItem.SetModification(UserName);
            _countyPromotionGiftOrderRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<CountyPromotionGiftOrderViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CountyPromotionGiftOrderViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _countyPromotionGiftOrderRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<CountyPromotionGiftOrderViewModel>(dbItem);
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
            var model = await _countyPromotionGiftOrderRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _countyPromotionGiftOrderRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("ByDateAndCountyAgent/{date}/{countyAgentId}")]
        public async Task<IActionResult> GetByDateAndCountyAgent(DateTime date, int countyAgentId)
        {
            var items = await _countyPromotionGiftOrderRepository.AllIncluding(x => x.CountyOrder, x => x.CountyPromotionEventBonus)
                .Where(x => x.CountyPromotionEventBonus.CountyPromotionEvent.Date == date && x.CountyOrder.CountyAgentId == countyAgentId)
                .ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyPromotionGiftOrderViewModel>>(items);
            return Ok(results);
        }
    }
}

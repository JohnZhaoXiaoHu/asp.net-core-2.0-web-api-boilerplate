using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Infrastructure.Services;
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
    public class RetailPromotionEventBonusController : SalesController<RetailPromotionEventBonusController>
    {
        private readonly IRetailPromotionEventBonusRepository _retailPromotionEventBonusRepository;
        public RetailPromotionEventBonusController(ICoreService<RetailPromotionEventBonusController> coreService,
            IRetailPromotionEventBonusRepository retailPromotionEventBonusRepository) : base(coreService)
        {
            _retailPromotionEventBonusRepository = retailPromotionEventBonusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _retailPromotionEventBonusRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<RetailPromotionEventBonusViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetRetailPromotionEventBonus")]
        public async Task<IActionResult> Get(int id)
        {
             var item = await _retailPromotionEventBonusRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<RetailPromotionEventBonusViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RetailPromotionEventBonusViewModel retailPromotionEventBonusVm)
        {
            if (retailPromotionEventBonusVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<RetailPromotionEventBonus>(retailPromotionEventBonusVm);
            newItem.SetCreation(UserName);
            _retailPromotionEventBonusRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<RetailPromotionEventBonusViewModel>(newItem);

            return CreatedAtRoute("GetRetailPromotionEventBonus", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RetailPromotionEventBonusViewModel retailPromotionEventBonusVm)
        {
            if (retailPromotionEventBonusVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _retailPromotionEventBonusRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(retailPromotionEventBonusVm, dbItem);
            dbItem.SetModification(UserName);
            _retailPromotionEventBonusRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<RetailPromotionEventBonusViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _retailPromotionEventBonusRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<RetailPromotionEventBonusViewModel>(dbItem);
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
            var model = await _retailPromotionEventBonusRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _retailPromotionEventBonusRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }
    }
}

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
    public class RetailPromotionSeriesBonusController : SalesController<RetailPromotionSeriesBonusController>
    {
        private readonly IRetailPromotionSeriesBonusRepository _retailPromotionSeriesBonusRepository;
        public RetailPromotionSeriesBonusController(ICoreService<RetailPromotionSeriesBonusController> coreService,
            IRetailPromotionSeriesBonusRepository retailPromotionSeriesBonusRepository) : base(coreService)
        {
            _retailPromotionSeriesBonusRepository = retailPromotionSeriesBonusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _retailPromotionSeriesBonusRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<RetailPromotionSeriesBonusViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetRetailPromotionSeriesBonus")]
        public async Task<IActionResult> Get(int id)
        {
             var item = await _retailPromotionSeriesBonusRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<RetailPromotionSeriesBonusViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RetailPromotionSeriesBonusViewModel retailPromotionSeriesBonusVm)
        {
            if (retailPromotionSeriesBonusVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<RetailPromotionSeriesBonus>(retailPromotionSeriesBonusVm);
            newItem.SetCreation(UserName);
            _retailPromotionSeriesBonusRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<RetailPromotionSeriesBonusViewModel>(newItem);

            return CreatedAtRoute("GetRetailPromotionSeriesBonus", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RetailPromotionSeriesBonusViewModel retailPromotionSeriesBonusVm)
        {
            if (retailPromotionSeriesBonusVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _retailPromotionSeriesBonusRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(retailPromotionSeriesBonusVm, dbItem);
            dbItem.SetModification(UserName);
            _retailPromotionSeriesBonusRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<RetailPromotionSeriesBonusViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _retailPromotionSeriesBonusRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<RetailPromotionSeriesBonusViewModel>(dbItem);
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
            var model = await _retailPromotionSeriesBonusRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _retailPromotionSeriesBonusRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }
    }
}

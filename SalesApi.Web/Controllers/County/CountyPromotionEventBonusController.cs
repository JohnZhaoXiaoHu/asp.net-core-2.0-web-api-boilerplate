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
    public class CountyPromotionEventBonusController : CountyController<CountyPromotionEventBonusController>
    {
        private readonly ICountyPromotionEventBonusRepository _countyPromotionEventBonusRepository;
        public CountyPromotionEventBonusController(ICountyService<CountyPromotionEventBonusController> countyService,
            ICountyPromotionEventBonusRepository countyPromotionEventBonusRepository) : base(countyService)
        {
            _countyPromotionEventBonusRepository = countyPromotionEventBonusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _countyPromotionEventBonusRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyPromotionEventBonusViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetCountyPromotionEventBonus")]
        public async Task<IActionResult> Get(int id)
        {
             var item = await _countyPromotionEventBonusRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<CountyPromotionEventBonusViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CountyPromotionEventBonusViewModel countyPromotionEventBonusVm)
        {
            if (countyPromotionEventBonusVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<CountyPromotionEventBonus>(countyPromotionEventBonusVm);
            newItem.SetCreation(UserName);
            _countyPromotionEventBonusRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<CountyPromotionEventBonusViewModel>(newItem);

            return CreatedAtRoute("GetCountyPromotionEventBonus", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CountyPromotionEventBonusViewModel countyPromotionEventBonusVm)
        {
            if (countyPromotionEventBonusVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _countyPromotionEventBonusRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(countyPromotionEventBonusVm, dbItem);
            dbItem.SetModification(UserName);
            _countyPromotionEventBonusRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CountyPromotionEventBonusViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _countyPromotionEventBonusRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<CountyPromotionEventBonusViewModel>(dbItem);
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
            var model = await _countyPromotionEventBonusRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _countyPromotionEventBonusRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
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
    public class RetailPromotionSeriesController : SalesController<RetailPromotionSeriesController>
    {
        private readonly IRetailPromotionSeriesRepository _retailPromotionSeriesRepository;
        private readonly IRetailPromotionEventRepository _retailPromotionEventRepository;

        public RetailPromotionSeriesController(ICoreService<RetailPromotionSeriesController> coreService,
            IRetailPromotionSeriesRepository retailPromotionSeriesRepository,
            IRetailPromotionEventRepository retailPromotionEventRepository) : base(coreService)
        {
            _retailPromotionSeriesRepository = retailPromotionSeriesRepository;
            _retailPromotionEventRepository = retailPromotionEventRepository;
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

            var newItem = Mapper.Map<RetailPromotionSeries>(retailPromotionSeriesVm);
            newItem.SetCreation(UserName);
            foreach (var newItemRetailPromotionSeriesBonuse in newItem.RetailPromotionSeriesBonuses)
            {
                newItemRetailPromotionSeriesBonuse.SetCreation(UserName);
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
        public async Task<IActionResult> Put(int id, [FromBody] RetailPromotionSeriesViewModel retailPromotionSeriesVm)
        {
            if (retailPromotionSeriesVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _retailPromotionSeriesRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(retailPromotionSeriesVm, dbItem);
            dbItem.SetModification(UserName);
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
            var model = await _retailPromotionSeriesRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _retailPromotionSeriesRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }
    }
}

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
    public class CountyPromotionSeriesBonusController : CountyController<CountyPromotionSeriesBonusController>
    {
        private readonly ICountyPromotionSeriesBonusRepository _countyPromotionSeriesBonusRepository;
        public CountyPromotionSeriesBonusController(ICountyService<CountyPromotionSeriesBonusController> countyService,
            ICountyPromotionSeriesBonusRepository countyPromotionSeriesBonusRepository) : base(countyService)
        {
            _countyPromotionSeriesBonusRepository = countyPromotionSeriesBonusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _countyPromotionSeriesBonusRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyPromotionSeriesBonusViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetCountyPromotionSeriesBonus")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _countyPromotionSeriesBonusRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<CountyPromotionSeriesBonusViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CountyPromotionSeriesBonusViewModel countyPromotionSeriesBonusVm)
        {
            if (countyPromotionSeriesBonusVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<CountyPromotionSeriesBonus>(countyPromotionSeriesBonusVm);
            newItem.SetCreation(UserName);
            _countyPromotionSeriesBonusRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<CountyPromotionSeriesBonusViewModel>(newItem);

            return CreatedAtRoute("GetCountyPromotionSeriesBonus", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CountyPromotionSeriesBonusViewModel countyPromotionSeriesBonusVm)
        {
            if (countyPromotionSeriesBonusVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _countyPromotionSeriesBonusRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(countyPromotionSeriesBonusVm, dbItem);
            dbItem.SetModification(UserName);
            _countyPromotionSeriesBonusRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CountyPromotionSeriesBonusViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _countyPromotionSeriesBonusRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<CountyPromotionSeriesBonusViewModel>(dbItem);
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
            var model = await _countyPromotionSeriesBonusRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _countyPromotionSeriesBonusRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("ByCountyPromotionSeries/{countyPromotionSeriesId}")]
        public async Task<IActionResult> GetByCountyPromotionSeries(int countyPromotionSeriesId)
        {
            var items = await _countyPromotionSeriesBonusRepository
                .All.Where(x => x.CountyPromotionSeriesId == countyPromotionSeriesId)
                .ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyPromotionSeriesBonusViewModel>>(items);
            return Ok(results);
        }

    }
}

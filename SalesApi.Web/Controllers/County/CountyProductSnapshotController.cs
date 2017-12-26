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
    public class CountyProductSnapshotController : CountyController<CountyProductSnapshotController>
    {
        private readonly ICountyProductSnapshotRepository _countyProductSnapshotRepository;
        public CountyProductSnapshotController(ICountyService<CountyProductSnapshotController> countyService,
            ICountyProductSnapshotRepository countyProductSnapshotRepository) : base(countyService)
        {
            _countyProductSnapshotRepository = countyProductSnapshotRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _countyProductSnapshotRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyProductSnapshotViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetCountyProductSnapshot")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _countyProductSnapshotRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<CountyProductSnapshotViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CountyProductSnapshotViewModel countyProductSnapshotVm)
        {
            if (countyProductSnapshotVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<CountyProductSnapshot>(countyProductSnapshotVm);
            newItem.SetCreation(UserName);
            _countyProductSnapshotRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<CountyProductSnapshotViewModel>(newItem);

            return CreatedAtRoute("GetCountyProductSnapshot", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CountyProductSnapshotViewModel countyProductSnapshotVm)
        {
            if (countyProductSnapshotVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _countyProductSnapshotRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(countyProductSnapshotVm, dbItem);
            dbItem.SetModification(UserName);
            _countyProductSnapshotRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CountyProductSnapshotViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _countyProductSnapshotRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<CountyProductSnapshotViewModel>(dbItem);
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
            var model = await _countyProductSnapshotRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _countyProductSnapshotRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("ByDate/{date?}")]
        public async Task<IActionResult> GetByDate(DateTime? date = null)
        {
            var dateStr = GetDateString(date);
            var items = await _countyProductSnapshotRepository.All.Where(x => x.CountyDay.Date == dateStr)
                .OrderBy(x => x.ProductForCounty.Product.Order).ToListAsync();
            var vms = Mapper.Map<IEnumerable<CountyProductSnapshotViewModel>>(items);
            return Ok(vms);
        }
    }
}

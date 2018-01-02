using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Mall;
using SalesApi.Repositories.Mall;
using SalesApi.Services.Mall;
using SalesApi.ViewModels.Mall;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Mall
{
    [Route("api/sales/[controller]")]
    public class MallProductSnapshotController : MallController<MallProductSnapshotController>
    {
        private readonly IMallProductSnapshotRepository _mallProductSnapshotRepository;
        public MallProductSnapshotController(IMallService<MallProductSnapshotController> mallService,
            IMallProductSnapshotRepository mallProductSnapshotRepository) : base(mallService)
        {
            _mallProductSnapshotRepository = mallProductSnapshotRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _mallProductSnapshotRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<MallProductSnapshotViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetMallProductSnapshot")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _mallProductSnapshotRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<MallProductSnapshotViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MallProductSnapshotViewModel mallProductSnapshotVm)
        {
            if (mallProductSnapshotVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<MallProductSnapshot>(mallProductSnapshotVm);
            newItem.SetCreation(UserName);
            _mallProductSnapshotRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<MallProductSnapshotViewModel>(newItem);

            return CreatedAtRoute("GetMallProductSnapshot", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MallProductSnapshotViewModel mallProductSnapshotVm)
        {
            if (mallProductSnapshotVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _mallProductSnapshotRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(mallProductSnapshotVm, dbItem);
            dbItem.SetModification(UserName);
            _mallProductSnapshotRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<MallProductSnapshotViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _mallProductSnapshotRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<MallProductSnapshotViewModel>(dbItem);
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
            var model = await _mallProductSnapshotRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _mallProductSnapshotRepository.Delete(model);
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
            var items = await _mallProductSnapshotRepository.All.Where(x => x.MallDay.Date == dateStr)
                .OrderBy(x => x.ProductForMall.Product.Order).ToListAsync();
            var vms = Mapper.Map<IEnumerable<MallProductSnapshotViewModel>>(items);
            return Ok(vms);
        }
    }
}

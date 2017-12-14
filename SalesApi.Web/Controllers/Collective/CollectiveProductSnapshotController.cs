using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Collective;
using SalesApi.Repositories.Collective;
using SalesApi.Services.Collective;
using SalesApi.ViewModels.Collective;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Collective
{
    [Route("api/sales/[controller]")]
    public class CollectiveProductSnapshotController : CollectiveController<CollectiveProductSnapshotController>
    {
        private readonly ICollectiveProductSnapshotRepository _collectiveProductSnapshotRepository;
        public CollectiveProductSnapshotController(ICollectiveService<CollectiveProductSnapshotController> collectiveService,
            ICollectiveProductSnapshotRepository collectiveProductSnapshotRepository) : base(collectiveService)
        {
            _collectiveProductSnapshotRepository = collectiveProductSnapshotRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _collectiveProductSnapshotRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<CollectiveProductSnapshotViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetCollectiveProductSnapshot")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _collectiveProductSnapshotRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<CollectiveProductSnapshotViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CollectiveProductSnapshotViewModel collectiveProductSnapshotVm)
        {
            if (collectiveProductSnapshotVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<CollectiveProductSnapshot>(collectiveProductSnapshotVm);
            newItem.SetCreation(UserName);
            _collectiveProductSnapshotRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<CollectiveProductSnapshotViewModel>(newItem);

            return CreatedAtRoute("GetCollectiveProductSnapshot", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CollectiveProductSnapshotViewModel collectiveProductSnapshotVm)
        {
            if (collectiveProductSnapshotVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _collectiveProductSnapshotRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(collectiveProductSnapshotVm, dbItem);
            dbItem.SetModification(UserName);
            _collectiveProductSnapshotRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CollectiveProductSnapshotViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _collectiveProductSnapshotRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<CollectiveProductSnapshotViewModel>(dbItem);
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
            var model = await _collectiveProductSnapshotRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _collectiveProductSnapshotRepository.Delete(model);
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
            var items = await _collectiveProductSnapshotRepository.All.Where(x => x.CollectiveDay.Date == dateStr).ToListAsync();
            var vms = Mapper.Map<IEnumerable<CollectiveProductSnapshotViewModel>>(items);
            return Ok(vms);
        }
    }
}

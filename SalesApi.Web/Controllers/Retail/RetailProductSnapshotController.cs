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
    public class RetailProductSnapshotController : RetailController<RetailProductSnapshotController>
    {
        private readonly IRetailProductSnapshotRepository _retailProductSnapshotRepository;
        public RetailProductSnapshotController(IRetailService<RetailProductSnapshotController> retailService,
            IRetailProductSnapshotRepository retailProductSnapshotRepository) : base(retailService)
        {
            _retailProductSnapshotRepository = retailProductSnapshotRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _retailProductSnapshotRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<RetailProductSnapshotViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetRetailProductSnapshot")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _retailProductSnapshotRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<RetailProductSnapshotViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RetailProductSnapshotViewModel retailProductSnapshotVm)
        {
            if (retailProductSnapshotVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<RetailProductSnapshot>(retailProductSnapshotVm);
            newItem.SetCreation(UserName);
            _retailProductSnapshotRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<RetailProductSnapshotViewModel>(newItem);

            return CreatedAtRoute("GetRetailProductSnapshot", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RetailProductSnapshotViewModel retailProductSnapshotVm)
        {
            if (retailProductSnapshotVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _retailProductSnapshotRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(retailProductSnapshotVm, dbItem);
            dbItem.SetModification(UserName);
            _retailProductSnapshotRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<RetailProductSnapshotViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _retailProductSnapshotRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<RetailProductSnapshotViewModel>(dbItem);
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
            var model = await _retailProductSnapshotRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _retailProductSnapshotRepository.Delete(model);
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
            var items = await _retailProductSnapshotRepository.All.Where(x => x.RetailDay.Date == dateStr).OrderBy(x => x.ProductForRetail.Product.Order).ToListAsync();
            var vms = Mapper.Map<IEnumerable<RetailProductSnapshotViewModel>>(items);
            return Ok(vms);
        }
    }
}

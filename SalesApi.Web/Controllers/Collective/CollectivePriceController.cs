using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Infrastructure.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Collective;
using SalesApi.Repositories.Collective;
using SalesApi.ViewModels.Collective;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Collective
{
    [Route("api/sales/[controller]")]
    public class CollectivePriceController : SalesController<CollectivePriceController>
    {
        private readonly ICollectivePriceRepository _collectivePriceRepository;
        public CollectivePriceController(ICoreService<CollectivePriceController> coreService,
            ICollectivePriceRepository collectivePriceRepository) : base(coreService)
        {
            _collectivePriceRepository = collectivePriceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _collectivePriceRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<CollectivePriceViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetCollectivePrice")]
        public async Task<IActionResult> Get(int id)
        {
             var item = await _collectivePriceRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<CollectivePriceViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CollectivePriceViewModel collectivePriceVm)
        {
            if (collectivePriceVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<CollectivePrice>(collectivePriceVm);
            newItem.SetCreation(UserName);
            _collectivePriceRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<CollectivePriceViewModel>(newItem);

            return CreatedAtRoute("GetCollectivePrice", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CollectivePriceViewModel collectivePriceVm)
        {
            if (collectivePriceVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _collectivePriceRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(collectivePriceVm, dbItem);
            dbItem.SetModification(UserName);
            _collectivePriceRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CollectivePriceViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _collectivePriceRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<CollectivePriceViewModel>(dbItem);
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
            var model = await _collectivePriceRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _collectivePriceRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("ByCollectiveCustomer/{collectiveCustomerId}")]
        public async Task<IActionResult> GetByCollectiveCustomer(int collectiveCustomerId)
        {
            var items = await _collectivePriceRepository.All.Where(x => x.CollectiveCustomerId == collectiveCustomerId).ToListAsync();
            var results = Mapper.Map<IEnumerable<CollectivePriceViewModel>>(items);
            return Ok(results);
        }

    }
}

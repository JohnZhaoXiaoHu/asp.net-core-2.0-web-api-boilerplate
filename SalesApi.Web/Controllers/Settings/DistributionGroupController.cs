using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Infrastructure.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Settings;
using SalesApi.Repositories.Settings;
using SalesApi.ViewModels.Settings;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Settings
{
    [Route("api/sales/[controller]")]
    public class DistributionGroupController : SalesController<DistributionGroupController>
    {
        private readonly IDistributionGroupRepository _distributionGroupRepository;
        public DistributionGroupController(ICoreService<DistributionGroupController> coreService,
            IDistributionGroupRepository distributionGroupRepository) : base(coreService)
        {
            _distributionGroupRepository = distributionGroupRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _distributionGroupRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<DistributionGroupViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetDistributionGroup")]
        public async Task<IActionResult> Get(int id)
        {
             var item = await _distributionGroupRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<DistributionGroupViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DistributionGroupViewModel distributionGroupVm)
        {
            if (distributionGroupVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<DistributionGroup>(distributionGroupVm);
            newItem.SetCreation(UserName);
            _distributionGroupRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<DistributionGroupViewModel>(newItem);

            return CreatedAtRoute("GetDistributionGroup", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DistributionGroupViewModel distributionGroupVm)
        {
            if (distributionGroupVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _distributionGroupRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(distributionGroupVm, dbItem);
            dbItem.SetModification(UserName);
            _distributionGroupRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<DistributionGroupViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _distributionGroupRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<DistributionGroupViewModel>(dbItem);
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
            var model = await _distributionGroupRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _distributionGroupRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Infrastructure.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Mall;
using SalesApi.Repositories.Mall;
using SalesApi.ViewModels.Mall;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Mall
{
    [Route("api/sales/[controller]")]
    public class MallGroupController : SalesController<MallGroupController>
    {
        private readonly IMallGroupRepository _mallGroupRepository;
        public MallGroupController(ICoreService<MallGroupController> coreService,
            IMallGroupRepository mallGroupRepository) : base(coreService)
        {
            _mallGroupRepository = mallGroupRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _mallGroupRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<MallGroupViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetMallGroup")]
        public async Task<IActionResult> Get(int id)
        {
             var item = await _mallGroupRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<MallGroupViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MallGroupViewModel mallGroupVm)
        {
            if (mallGroupVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<MallGroup>(mallGroupVm);
            newItem.SetCreation(UserName);
            _mallGroupRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<MallGroupViewModel>(newItem);

            return CreatedAtRoute("GetMallGroup", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MallGroupViewModel mallGroupVm)
        {
            if (mallGroupVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _mallGroupRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(mallGroupVm, dbItem);
            dbItem.SetModification(UserName);
            _mallGroupRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<MallGroupViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _mallGroupRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<MallGroupViewModel>(dbItem);
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
            var model = await _mallGroupRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _mallGroupRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }
    }
}

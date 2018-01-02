using System.Collections.Generic;
using System.Linq;
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
    public class MallPriceController : SalesController<MallPriceController>
    {
        private readonly IMallPriceRepository _mallPriceRepository;
        public MallPriceController(ICoreService<MallPriceController> coreService,
            IMallPriceRepository mallPriceRepository) : base(coreService)
        {
            _mallPriceRepository = mallPriceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _mallPriceRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<MallPriceViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetMallPrice")]
        public async Task<IActionResult> Get(int id)
        {
             var item = await _mallPriceRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<MallPriceViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MallPriceViewModel mallPriceVm)
        {
            if (mallPriceVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<MallPrice>(mallPriceVm);
            newItem.SetCreation(UserName);
            _mallPriceRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<MallPriceViewModel>(newItem);

            return CreatedAtRoute("GetMallPrice", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MallPriceViewModel mallPriceVm)
        {
            if (mallPriceVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _mallPriceRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(mallPriceVm, dbItem);
            dbItem.SetModification(UserName);
            _mallPriceRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<MallPriceViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _mallPriceRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<MallPriceViewModel>(dbItem);
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
            var model = await _mallPriceRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _mallPriceRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("ByMallCustomer/{mallCustomerId}")]
        public async Task<IActionResult> GetByMallCustomer(int mallCustomerId)
        {
            var items = await _mallPriceRepository.All.Where(x => x.MallCustomerId == mallCustomerId).ToListAsync();
            var results = Mapper.Map<IEnumerable<MallPriceViewModel>>(items);
            return Ok(results);
        }

    }
}

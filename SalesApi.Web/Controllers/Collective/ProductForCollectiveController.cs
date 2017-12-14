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
    public class ProductForCollectiveController : CollectiveController<ProductForCollectiveController>
    {
        private readonly IProductForCollectiveRepository _productForCollectiveRepository;

        public ProductForCollectiveController(ICollectiveService<ProductForCollectiveController> collectiveService,
            IProductForCollectiveRepository productForCollectiveRepository) : base(collectiveService)
        {
            _productForCollectiveRepository = productForCollectiveRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _productForCollectiveRepository.AllIncluding(x => x.Product).ToListAsync();
            var results = Mapper.Map<IEnumerable<ProductForCollectiveViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetProductForCollective")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _productForCollectiveRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<ProductForCollectiveViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductForCollectiveViewModel productForCollectiveVm)
        {
            if (productForCollectiveVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<ProductForCollective>(productForCollectiveVm);
            newItem.SetCreation(UserName);
            _productForCollectiveRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<ProductForCollectiveViewModel>(newItem);

            return CreatedAtRoute("GetProductForCollective", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductForCollectiveViewModel productForCollectiveVm)
        {
            if (productForCollectiveVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _productForCollectiveRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(productForCollectiveVm, dbItem);
            dbItem.SetModification(UserName);
            _productForCollectiveRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<ProductForCollectiveViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<ProductForCollectiveViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _productForCollectiveRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<ProductForCollectiveViewModel>(dbItem);
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
            var model = await _productForCollectiveRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _productForCollectiveRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("NotDeleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var items = await _productForCollectiveRepository.AllIncluding(x => x.Product).Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<ProductForCollectiveViewModel>>(items);
            return Ok(results);
        }
    }
}

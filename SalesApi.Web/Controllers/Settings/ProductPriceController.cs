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
    public class ProductPriceController : SalesController<ProductPriceController>
    {
        private readonly IProductPriceRepository _productPriceRepository;
        public ProductPriceController(ICoreService<ProductPriceController> coreService,
            IProductPriceRepository productPriceRepository) : base(coreService)
        {
            _productPriceRepository = productPriceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _productPriceRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<ProductPriceViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetProductPrice")]
        public async Task<IActionResult> Get(int id)
        {
             var item = await _productPriceRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<ProductPriceViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductPriceViewModel productPriceVm)
        {
            if (productPriceVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<ProductPrice>(productPriceVm);
            newItem.SetCreation(UserName);
            _productPriceRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<ProductPriceViewModel>(newItem);

            return CreatedAtRoute("GetProductPrice", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductPriceViewModel productPriceVm)
        {
            if (productPriceVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _productPriceRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(productPriceVm, dbItem);
            dbItem.SetModification(UserName);
            _productPriceRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<ProductPriceViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _productPriceRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<ProductPriceViewModel>(dbItem);
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
            var model = await _productPriceRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _productPriceRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }
    }
}

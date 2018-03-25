using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Core.DomainModels;
using SalesApi.Core.IRepositories.Settings;
using SalesApi.Core.Services;
using SalesApi.Shared.Enums;
using SalesApi.ViewModels.Settings;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Settings
{
    [Route("api/sales/[controller]")]
    public class ProductController : SalesController<ProductController>
    {
        private readonly IProductRepository _productRepository;
        public ProductController(ICoreService<ProductController> coreService,
            IProductRepository productRepository) : base(coreService)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _productRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<ProductViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetProduct")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _productRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<ProductViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductCreationViewModel productVm)
        {
            if (productVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<Product>(productVm);
            _productRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<ProductViewModel>(newItem);

            return CreatedAtRoute("GetProduct", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductModificationViewModel productVm)
        {
            if (productVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _productRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(productVm, dbItem);
            _productRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<ProductModificationViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _productRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<ProductModificationViewModel>(dbItem);
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
            var model = await _productRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _productRepository.Delete(model);
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
            var items = await _productRepository.All.Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<ProductViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("ProductUnits")]
        public IActionResult GetProductUnits()
        {
            var items = Enum.GetValues(typeof(ProductUnit)).Cast<ProductUnit>()
                .Select(x => new KeyValuePair<string, int>(x.ToString(), (int)x));
            return Ok(items);
        }

    }
}

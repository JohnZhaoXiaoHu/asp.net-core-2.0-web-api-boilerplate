using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Infrastructure.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Retail;
using SalesApi.Repositories.Retail;
using SalesApi.ViewModels.Retail;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Retail
{
    [Route("api/sales/[controller]")]
    public class ProductForRetailController : SalesController<ProductForRetailController>
    {
        private readonly IProductForRetailRepository _productForRetailRepository;
        public ProductForRetailController(ICoreService<ProductForRetailController> coreService,
            IProductForRetailRepository productForRetailRepository) : base(coreService)
        {
            _productForRetailRepository = productForRetailRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _productForRetailRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<ProductForRetailViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetProductForRetail")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _productForRetailRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<ProductForRetailViewModel>(item);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductForRetailViewModel productForRetailVm)
        {
            if (productForRetailVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<ProductForRetail>(productForRetailVm);
            _productForRetailRepository.SetPrice(newItem);
            newItem.SetCreation(UserName);
            _productForRetailRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<ProductForRetailViewModel>(newItem);

            return CreatedAtRoute("GetProductForRetail", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductForRetailViewModel productForRetailVm)
        {
            if (productForRetailVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _productForRetailRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(productForRetailVm, dbItem);
            _productForRetailRepository.SetPrice(dbItem);
            dbItem.SetModification(UserName);
            _productForRetailRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<ProductForRetailViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<ProductForRetailViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _productForRetailRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<ProductForRetailViewModel>(dbItem);
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
            var model = await _productForRetailRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _productForRetailRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }
    }
}

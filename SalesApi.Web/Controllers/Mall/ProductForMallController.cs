using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Mall;
using SalesApi.Repositories.Mall;
using SalesApi.Services.Mall;
using SalesApi.ViewModels.Mall;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Mall
{
    [Route("api/sales/[controller]")]
    public class ProductForMallController : MallController<ProductForMallController>
    {
        private readonly IProductForMallRepository _productForMallRepository;

        public ProductForMallController(IMallService<ProductForMallController> mallService,
            IProductForMallRepository productForMallRepository) : base(mallService)
        {
            _productForMallRepository = productForMallRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _productForMallRepository.AllIncluding(x => x.Product).ToListAsync();
            var results = Mapper.Map<IEnumerable<ProductForMallViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetProductForMall")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _productForMallRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<ProductForMallViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductForMallViewModel productForMallVm)
        {
            if (productForMallVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<ProductForMall>(productForMallVm);
            newItem.SetCreation(UserName);
            _productForMallRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<ProductForMallViewModel>(newItem);

            return CreatedAtRoute("GetProductForMall", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductForMallViewModel productForMallVm)
        {
            if (productForMallVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _productForMallRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(productForMallVm, dbItem);
            dbItem.SetModification(UserName);
            _productForMallRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<ProductForMallViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<ProductForMallViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _productForMallRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<ProductForMallViewModel>(dbItem);
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
            var model = await _productForMallRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _productForMallRepository.Delete(model);
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
            var items = await _productForMallRepository.AllIncluding(x => x.Product).Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<ProductForMallViewModel>>(items);
            return Ok(results);
        }
    }
}

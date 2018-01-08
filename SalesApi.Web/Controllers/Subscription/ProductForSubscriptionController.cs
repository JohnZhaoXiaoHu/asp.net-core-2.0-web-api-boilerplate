using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Subscription;
using SalesApi.Repositories.Subscription;
using SalesApi.Services.Subscription;
using SalesApi.ViewModels.Subscription;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Subscription
{
    [Route("api/sales/[controller]")]
    public class ProductForSubscriptionController : SubscriptionController<ProductForSubscriptionController>
    {
        private readonly IProductForSubscriptionRepository _productForSubscriptionRepository;
        public ProductForSubscriptionController(ISubscriptionService<ProductForSubscriptionController> subscriptionService,
            IProductForSubscriptionRepository productForSubscriptionRepository) : base(subscriptionService)
        {
            _productForSubscriptionRepository = productForSubscriptionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _productForSubscriptionRepository.AllIncluding(x => x.Product).ToListAsync();
            var results = Mapper.Map<IEnumerable<ProductForSubscriptionViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetProductForSubscription")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _productForSubscriptionRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<ProductForSubscriptionViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductForSubscriptionViewModel productForSubscriptionVm)
        {
            if (productForSubscriptionVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<ProductForSubscription>(productForSubscriptionVm);
            _productForSubscriptionRepository.SetPrice(newItem);
            newItem.SetCreation(UserName);
            _productForSubscriptionRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<ProductForSubscriptionViewModel>(newItem);

            return CreatedAtRoute("GetProductForSubscription", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductForSubscriptionViewModel productForSubscriptionVm)
        {
            if (productForSubscriptionVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _productForSubscriptionRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(productForSubscriptionVm, dbItem);
            _productForSubscriptionRepository.SetPrice(dbItem);
            dbItem.SetModification(UserName);
            _productForSubscriptionRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<ProductForSubscriptionViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<ProductForSubscriptionViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _productForSubscriptionRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<ProductForSubscriptionViewModel>(dbItem);
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
            var model = await _productForSubscriptionRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _productForSubscriptionRepository.Delete(model);
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
            var items = await _productForSubscriptionRepository.AllIncluding(x => x.Product).Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<ProductForSubscriptionViewModel>>(items);
            return Ok(results);
        }
    }
}

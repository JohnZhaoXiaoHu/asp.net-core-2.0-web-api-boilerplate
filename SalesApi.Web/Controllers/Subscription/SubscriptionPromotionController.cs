using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Subscription.Promotion;
using SalesApi.Repositories.Subscription.Promotion;
using SalesApi.Services.Subscription;
using SalesApi.ViewModels.Subscription.Promotion;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Subscription
{
    [Route("api/sales/[controller]")]
    public class SubscriptionPromotionController : SubscriptionController<SubscriptionPromotionController>
    {
        private readonly ISubscriptionPromotionRepository _subscriptionPromotionRepository;
        public SubscriptionPromotionController(ISubscriptionService<SubscriptionPromotionController> subscriptionService,
            ISubscriptionPromotionRepository subscriptionPromotionRepository) : base(subscriptionService)
        {
            _subscriptionPromotionRepository = subscriptionPromotionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _subscriptionPromotionRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionPromotionViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubscriptionPromotion")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _subscriptionPromotionRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SubscriptionPromotionViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionPromotionViewModel subscriptionPromotionVm)
        {
            if (subscriptionPromotionVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<SubscriptionPromotion>(subscriptionPromotionVm);
            newItem.SetCreation(UserName);
            _subscriptionPromotionRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<SubscriptionPromotionViewModel>(newItem);

            return CreatedAtRoute("GetSubscriptionPromotion", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubscriptionPromotionViewModel subscriptionPromotionVm)
        {
            if (subscriptionPromotionVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _subscriptionPromotionRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(subscriptionPromotionVm, dbItem);
            dbItem.SetModification(UserName);
            _subscriptionPromotionRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<SubscriptionPromotionViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubscriptionPromotionViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _subscriptionPromotionRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SubscriptionPromotionViewModel>(dbItem);
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
            var model = await _subscriptionPromotionRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _subscriptionPromotionRepository.Delete(model);
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
            var items = await _subscriptionPromotionRepository.All.Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionPromotionViewModel>>(items);
            return Ok(results);
        }
    }
}

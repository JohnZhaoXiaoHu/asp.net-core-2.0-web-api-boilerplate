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

namespace SalesApi.Web.Controllers.Subscription.Promotion
{
    [Route("api/sales/[controller]")]
    public class SubscriptionPromotionMonthBonusDeliveryDateController : SubscriptionController<SubscriptionPromotionMonthBonusDeliveryDateController>
    {
        private readonly ISubscriptionPromotionMonthBonusDeliveryDateRepository _subscriptionPromotionMonthBonusDeliveryDateRepository;
        public SubscriptionPromotionMonthBonusDeliveryDateController(ISubscriptionService<SubscriptionPromotionMonthBonusDeliveryDateController> subscriptionService,
            ISubscriptionPromotionMonthBonusDeliveryDateRepository subscriptionPromotionMonthBonusDeliveryDateRepository) : base(subscriptionService)
        {
            _subscriptionPromotionMonthBonusDeliveryDateRepository = subscriptionPromotionMonthBonusDeliveryDateRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _subscriptionPromotionMonthBonusDeliveryDateRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionPromotionMonthBonusDeliveryDateViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubscriptionPromotionMonthBonusDeliveryDate")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _subscriptionPromotionMonthBonusDeliveryDateRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SubscriptionPromotionMonthBonusDeliveryDateViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionPromotionMonthBonusDeliveryDateViewModel subscriptionPromotionMonthBonusDeliveryDateVm)
        {
            if (subscriptionPromotionMonthBonusDeliveryDateVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<SubscriptionPromotionMonthBonusDeliveryDate>(subscriptionPromotionMonthBonusDeliveryDateVm);
            newItem.SetCreation(UserName);
            _subscriptionPromotionMonthBonusDeliveryDateRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<SubscriptionPromotionMonthBonusDeliveryDateViewModel>(newItem);

            return CreatedAtRoute("GetSubscriptionPromotionMonthBonusDeliveryDate", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubscriptionPromotionMonthBonusDeliveryDateViewModel subscriptionPromotionMonthBonusDeliveryDateVm)
        {
            if (subscriptionPromotionMonthBonusDeliveryDateVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _subscriptionPromotionMonthBonusDeliveryDateRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(subscriptionPromotionMonthBonusDeliveryDateVm, dbItem);
            dbItem.SetModification(UserName);
            _subscriptionPromotionMonthBonusDeliveryDateRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<SubscriptionPromotionMonthBonusDeliveryDateViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubscriptionPromotionMonthBonusDeliveryDateViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _subscriptionPromotionMonthBonusDeliveryDateRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SubscriptionPromotionMonthBonusDeliveryDateViewModel>(dbItem);
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
            var model = await _subscriptionPromotionMonthBonusDeliveryDateRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _subscriptionPromotionMonthBonusDeliveryDateRepository.Delete(model);
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
            var items = await _subscriptionPromotionMonthBonusDeliveryDateRepository.All.Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionPromotionMonthBonusDeliveryDateViewModel>>(items);
            return Ok(results);
        }
    }
}

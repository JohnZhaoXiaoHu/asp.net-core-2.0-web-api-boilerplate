using System;
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
    public class SubscriptionPromotionEventBonusController : SubscriptionController<SubscriptionPromotionEventBonusController>
    {
        private readonly ISubscriptionPromotionEventBonusRepository _subscriptionPromotionEventBonusRepository;
        public SubscriptionPromotionEventBonusController(ISubscriptionService<SubscriptionPromotionEventBonusController> subscriptionService,
            ISubscriptionPromotionEventBonusRepository subscriptionPromotionEventBonusRepository) : base(subscriptionService)
        {
            _subscriptionPromotionEventBonusRepository = subscriptionPromotionEventBonusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _subscriptionPromotionEventBonusRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionPromotionEventBonusViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubscriptionPromotionEventBonus")]
        public async Task<IActionResult> Get(int id)
        {
             var item = await _subscriptionPromotionEventBonusRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SubscriptionPromotionEventBonusViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionPromotionEventBonusViewModel subscriptionPromotionEventBonusVm)
        {
            if (subscriptionPromotionEventBonusVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<SubscriptionPromotionEventBonus>(subscriptionPromotionEventBonusVm);
            newItem.SetCreation(UserName);
            _subscriptionPromotionEventBonusRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<SubscriptionPromotionEventBonusViewModel>(newItem);

            return CreatedAtRoute("GetSubscriptionPromotionEventBonus", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubscriptionPromotionEventBonusViewModel subscriptionPromotionEventBonusVm)
        {
            if (subscriptionPromotionEventBonusVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _subscriptionPromotionEventBonusRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(subscriptionPromotionEventBonusVm, dbItem);
            dbItem.SetModification(UserName);
            _subscriptionPromotionEventBonusRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubscriptionPromotionEventBonusViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _subscriptionPromotionEventBonusRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SubscriptionPromotionEventBonusViewModel>(dbItem);
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
            var model = await _subscriptionPromotionEventBonusRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _subscriptionPromotionEventBonusRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }
    }
}

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
    public class SubscriptionMonthPromotionBonusController : SubscriptionController<SubscriptionMonthPromotionBonusController>
    {
        private readonly ISubscriptionMonthPromotionBonusRepository _subscriptionMonthPromotionBonusRepository;
        public SubscriptionMonthPromotionBonusController(ISubscriptionService<SubscriptionMonthPromotionBonusController> subscriptionService,
            ISubscriptionMonthPromotionBonusRepository subscriptionMonthPromotionBonusRepository) : base(subscriptionService)
        {
            _subscriptionMonthPromotionBonusRepository = subscriptionMonthPromotionBonusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _subscriptionMonthPromotionBonusRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionMonthPromotionBonusViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubscriptionMonthPromotionBonus")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _subscriptionMonthPromotionBonusRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SubscriptionMonthPromotionBonusViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionMonthPromotionBonusViewModel subscriptionMonthPromotionBonusVm)
        {
            if (subscriptionMonthPromotionBonusVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<SubscriptionMonthPromotionBonus>(subscriptionMonthPromotionBonusVm);
            newItem.SetCreation(UserName);
            _subscriptionMonthPromotionBonusRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<SubscriptionMonthPromotionBonusViewModel>(newItem);

            return CreatedAtRoute("GetSubscriptionMonthPromotionBonus", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubscriptionMonthPromotionBonusViewModel subscriptionMonthPromotionBonusVm)
        {
            if (subscriptionMonthPromotionBonusVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _subscriptionMonthPromotionBonusRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(subscriptionMonthPromotionBonusVm, dbItem);
            dbItem.SetModification(UserName);
            _subscriptionMonthPromotionBonusRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<SubscriptionMonthPromotionBonusViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubscriptionMonthPromotionBonusViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _subscriptionMonthPromotionBonusRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SubscriptionMonthPromotionBonusViewModel>(dbItem);
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
            var model = await _subscriptionMonthPromotionBonusRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _subscriptionMonthPromotionBonusRepository.Delete(model);
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
            var items = await _subscriptionMonthPromotionBonusRepository.All.Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionMonthPromotionBonusViewModel>>(items);
            return Ok(results);
        }
    }
}

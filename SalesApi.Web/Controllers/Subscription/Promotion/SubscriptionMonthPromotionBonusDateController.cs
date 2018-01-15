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
    public class SubscriptionMonthPromotionBonusDateController : SubscriptionController<SubscriptionMonthPromotionBonusDateController>
    {
        private readonly ISubscriptionMonthPromotionBonusDateRepository _subscriptionMonthPromotionBonusDateRepository;
        public SubscriptionMonthPromotionBonusDateController(ISubscriptionService<SubscriptionMonthPromotionBonusDateController> subscriptionService,
            ISubscriptionMonthPromotionBonusDateRepository subscriptionMonthPromotionBonusDateRepository) : base(subscriptionService)
        {
            _subscriptionMonthPromotionBonusDateRepository = subscriptionMonthPromotionBonusDateRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _subscriptionMonthPromotionBonusDateRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionMonthPromotionBonusDateViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubscriptionMonthPromotionBonusDate")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _subscriptionMonthPromotionBonusDateRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SubscriptionMonthPromotionBonusDateViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionMonthPromotionBonusDateViewModel subscriptionMonthPromotionBonusDateVm)
        {
            if (subscriptionMonthPromotionBonusDateVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<SubscriptionMonthPromotionBonusDate>(subscriptionMonthPromotionBonusDateVm);
            newItem.SetCreation(UserName);
            _subscriptionMonthPromotionBonusDateRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<SubscriptionMonthPromotionBonusDateViewModel>(newItem);

            return CreatedAtRoute("GetSubscriptionMonthPromotionBonusDate", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubscriptionMonthPromotionBonusDateViewModel subscriptionMonthPromotionBonusDateVm)
        {
            if (subscriptionMonthPromotionBonusDateVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _subscriptionMonthPromotionBonusDateRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(subscriptionMonthPromotionBonusDateVm, dbItem);
            dbItem.SetModification(UserName);
            _subscriptionMonthPromotionBonusDateRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<SubscriptionMonthPromotionBonusDateViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubscriptionMonthPromotionBonusDateViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _subscriptionMonthPromotionBonusDateRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SubscriptionMonthPromotionBonusDateViewModel>(dbItem);
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
            var model = await _subscriptionMonthPromotionBonusDateRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _subscriptionMonthPromotionBonusDateRepository.Delete(model);
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
            var items = await _subscriptionMonthPromotionBonusDateRepository.All.Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionMonthPromotionBonusDateViewModel>>(items);
            return Ok(results);
        }
    }
}

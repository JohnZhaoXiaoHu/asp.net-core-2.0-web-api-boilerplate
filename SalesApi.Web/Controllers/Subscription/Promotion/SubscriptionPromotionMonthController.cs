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
    public class SubscriptionPromotionMonthController : SubscriptionController<SubscriptionPromotionMonthController>
    {
        private readonly ISubscriptionPromotionMonthRepository _subscriptionPromotionMonthRepository;
        public SubscriptionPromotionMonthController(ISubscriptionService<SubscriptionPromotionMonthController> subscriptionService,
            ISubscriptionPromotionMonthRepository subscriptionPromotionMonthRepository) : base(subscriptionService)
        {
            _subscriptionPromotionMonthRepository = subscriptionPromotionMonthRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _subscriptionPromotionMonthRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionPromotionMonthViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubscriptionPromotionMonth")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _subscriptionPromotionMonthRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SubscriptionPromotionMonthViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionPromotionMonthViewModel subscriptionPromotionMonthVm)
        {
            if (subscriptionPromotionMonthVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<SubscriptionPromotionMonth>(subscriptionPromotionMonthVm);
            newItem.SetCreation(UserName);
            _subscriptionPromotionMonthRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<SubscriptionPromotionMonthViewModel>(newItem);

            return CreatedAtRoute("GetSubscriptionPromotionMonth", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubscriptionPromotionMonthViewModel subscriptionPromotionMonthVm)
        {
            if (subscriptionPromotionMonthVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _subscriptionPromotionMonthRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(subscriptionPromotionMonthVm, dbItem);
            dbItem.SetModification(UserName);
            _subscriptionPromotionMonthRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<SubscriptionPromotionMonthViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubscriptionPromotionMonthViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _subscriptionPromotionMonthRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SubscriptionPromotionMonthViewModel>(dbItem);
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
            var model = await _subscriptionPromotionMonthRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _subscriptionPromotionMonthRepository.Delete(model);
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
            var items = await _subscriptionPromotionMonthRepository.All.Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionPromotionMonthViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("ByYear/{year}")]
        public async Task<IActionResult> GetByYear(int year)
        {
            var items = await _subscriptionPromotionMonthRepository
                .AllIncluding(x => x.SubscriptionPromotion, x => x.SubscriptionPromotionMonthBonuses)
                .Where(x => x.Year == year && !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionPromotionMonthWithBonusViewModel>>(items);
            return Ok(results);
        }
    }
}

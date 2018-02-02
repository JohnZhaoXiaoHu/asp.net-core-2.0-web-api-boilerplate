using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Subscription.Order;
using SalesApi.Repositories.Subscription.Order;
using SalesApi.Services.Subscription;
using SalesApi.ViewModels.Subscription.Order;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Subscription.Order
{
    [Route("api/sales/[controller]")]
    public class SubscriptionOrderModifiedBonusDateController : SubscriptionController<SubscriptionOrderModifiedBonusDateController>
    {
        private readonly ISubscriptionOrderModifiedBonusDateRepository _subscriptionOrderModifiedBonusDateRepository;
        public SubscriptionOrderModifiedBonusDateController(ISubscriptionService<SubscriptionOrderModifiedBonusDateController> subscriptionService,
            ISubscriptionOrderModifiedBonusDateRepository subscriptionOrderModifiedBonusDateRepository) : base(subscriptionService)
        {
            _subscriptionOrderModifiedBonusDateRepository = subscriptionOrderModifiedBonusDateRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _subscriptionOrderModifiedBonusDateRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionOrderModifiedBonusDateViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubscriptionOrderModifiedBonusDate")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _subscriptionOrderModifiedBonusDateRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SubscriptionOrderModifiedBonusDateViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionOrderModifiedBonusDateViewModel subscriptionOrderModifiedBonusDateVm)
        {
            if (subscriptionOrderModifiedBonusDateVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<SubscriptionOrderModifiedBonusDate>(subscriptionOrderModifiedBonusDateVm);
            newItem.SetCreation(UserName);
            _subscriptionOrderModifiedBonusDateRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<SubscriptionOrderModifiedBonusDateViewModel>(newItem);

            return CreatedAtRoute("GetSubscriptionOrderModifiedBonusDate", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubscriptionOrderModifiedBonusDateViewModel subscriptionOrderModifiedBonusDateVm)
        {
            if (subscriptionOrderModifiedBonusDateVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _subscriptionOrderModifiedBonusDateRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(subscriptionOrderModifiedBonusDateVm, dbItem);
            dbItem.SetModification(UserName);
            _subscriptionOrderModifiedBonusDateRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<SubscriptionOrderModifiedBonusDateViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubscriptionOrderModifiedBonusDateViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _subscriptionOrderModifiedBonusDateRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SubscriptionOrderModifiedBonusDateViewModel>(dbItem);
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
            var model = await _subscriptionOrderModifiedBonusDateRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _subscriptionOrderModifiedBonusDateRepository.Delete(model);
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
            var items = await _subscriptionOrderModifiedBonusDateRepository.All.Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionOrderModifiedBonusDateViewModel>>(items);
            return Ok(results);
        }
    }
}

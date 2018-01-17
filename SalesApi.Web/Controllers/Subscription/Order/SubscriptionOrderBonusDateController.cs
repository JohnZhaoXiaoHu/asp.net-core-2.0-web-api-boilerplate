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
    public class SubscriptionOrderBonusDateController : SubscriptionController<SubscriptionOrderBonusDateController>
    {
        private readonly ISubscriptionOrderBonusDateRepository _subscriptionOrderBonusDateRepository;
        public SubscriptionOrderBonusDateController(ISubscriptionService<SubscriptionOrderBonusDateController> subscriptionService,
            ISubscriptionOrderBonusDateRepository subscriptionOrderBonusDateRepository) : base(subscriptionService)
        {
            _subscriptionOrderBonusDateRepository = subscriptionOrderBonusDateRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _subscriptionOrderBonusDateRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionOrderBonusDateViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubscriptionOrderBonusDate")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _subscriptionOrderBonusDateRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SubscriptionOrderBonusDateViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionOrderBonusDateViewModel subscriptionOrderBonusDateVm)
        {
            if (subscriptionOrderBonusDateVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<SubscriptionOrderBonusDate>(subscriptionOrderBonusDateVm);
            newItem.SetCreation(UserName);
            _subscriptionOrderBonusDateRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<SubscriptionOrderBonusDateViewModel>(newItem);

            return CreatedAtRoute("GetSubscriptionOrderBonusDate", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubscriptionOrderBonusDateViewModel subscriptionOrderBonusDateVm)
        {
            if (subscriptionOrderBonusDateVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _subscriptionOrderBonusDateRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(subscriptionOrderBonusDateVm, dbItem);
            dbItem.SetModification(UserName);
            _subscriptionOrderBonusDateRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<SubscriptionOrderBonusDateViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubscriptionOrderBonusDateViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _subscriptionOrderBonusDateRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SubscriptionOrderBonusDateViewModel>(dbItem);
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
            var model = await _subscriptionOrderBonusDateRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _subscriptionOrderBonusDateRepository.Delete(model);
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
            var items = await _subscriptionOrderBonusDateRepository.All.Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionOrderBonusDateViewModel>>(items);
            return Ok(results);
        }
    }
}

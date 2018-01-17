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
    public class SubscriptionOrderDateController : SubscriptionController<SubscriptionOrderDateController>
    {
        private readonly ISubscriptionOrderDateRepository _subscriptionOrderDateRepository;
        public SubscriptionOrderDateController(ISubscriptionService<SubscriptionOrderDateController> subscriptionService,
            ISubscriptionOrderDateRepository subscriptionOrderDateRepository) : base(subscriptionService)
        {
            _subscriptionOrderDateRepository = subscriptionOrderDateRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _subscriptionOrderDateRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionOrderDateViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubscriptionOrderDate")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _subscriptionOrderDateRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SubscriptionOrderDateViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionOrderDateViewModel subscriptionOrderDateVm)
        {
            if (subscriptionOrderDateVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<SubscriptionOrderDate>(subscriptionOrderDateVm);
            newItem.SetCreation(UserName);
            _subscriptionOrderDateRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<SubscriptionOrderDateViewModel>(newItem);

            return CreatedAtRoute("GetSubscriptionOrderDate", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubscriptionOrderDateViewModel subscriptionOrderDateVm)
        {
            if (subscriptionOrderDateVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _subscriptionOrderDateRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(subscriptionOrderDateVm, dbItem);
            dbItem.SetModification(UserName);
            _subscriptionOrderDateRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<SubscriptionOrderDateViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubscriptionOrderDateViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _subscriptionOrderDateRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SubscriptionOrderDateViewModel>(dbItem);
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
            var model = await _subscriptionOrderDateRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _subscriptionOrderDateRepository.Delete(model);
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
            var items = await _subscriptionOrderDateRepository.All.Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionOrderDateViewModel>>(items);
            return Ok(results);
        }
    }
}

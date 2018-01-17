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
    public class SubscriptionProductSnapshotController : SubscriptionController<SubscriptionProductSnapshotController>
    {
        private readonly ISubscriptionProductSnapshotRepository _subscriptionProductSnapshotRepository;
        public SubscriptionProductSnapshotController(ISubscriptionService<SubscriptionProductSnapshotController> subscriptionService,
            ISubscriptionProductSnapshotRepository subscriptionProductSnapshotRepository) : base(subscriptionService)
        {
            _subscriptionProductSnapshotRepository = subscriptionProductSnapshotRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _subscriptionProductSnapshotRepository.AllIncluding(x => x.ProductForSubscription).ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionProductSnapshotViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubscriptionProductSnapshot")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _subscriptionProductSnapshotRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SubscriptionProductSnapshotViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionProductSnapshotViewModel subscriptionProductSnapshotVm)
        {
            if (subscriptionProductSnapshotVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<SubscriptionProductSnapshot>(subscriptionProductSnapshotVm);
            newItem.SetCreation(UserName);
            _subscriptionProductSnapshotRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<SubscriptionProductSnapshotViewModel>(newItem);

            return CreatedAtRoute("GetSubscriptionProductSnapshot", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubscriptionProductSnapshotViewModel subscriptionProductSnapshotVm)
        {
            if (subscriptionProductSnapshotVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _subscriptionProductSnapshotRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(subscriptionProductSnapshotVm, dbItem);
            dbItem.SetModification(UserName);
            _subscriptionProductSnapshotRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubscriptionProductSnapshotViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _subscriptionProductSnapshotRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SubscriptionProductSnapshotViewModel>(dbItem);
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
            var model = await _subscriptionProductSnapshotRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _subscriptionProductSnapshotRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("ByDate/{date?}")]
        public async Task<IActionResult> GetByDate(DateTime? date = null)
        {
            var dateStr = GetDateString(date);
            var items = await _subscriptionProductSnapshotRepository.AllIncluding(x => x.ProductForSubscription)
                .Where(x => x.SubscriptionDay.Date == dateStr).OrderBy(x => x.ProductForSubscription.Product.Order).ToListAsync();
            var vms = Mapper.Map<IEnumerable<SubscriptionProductSnapshotViewModel>>(items);
            return Ok(vms);
        }
    }
}

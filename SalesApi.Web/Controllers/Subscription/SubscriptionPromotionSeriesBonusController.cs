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
    public class SubscriptionPromotionSeriesBonusController : SubscriptionController<SubscriptionPromotionSeriesBonusController>
    {
        private readonly ISubscriptionPromotionSeriesBonusRepository _subscriptionPromotionSeriesBonusRepository;
        public SubscriptionPromotionSeriesBonusController(ISubscriptionService<SubscriptionPromotionSeriesBonusController> subscriptionService,
            ISubscriptionPromotionSeriesBonusRepository subscriptionPromotionSeriesBonusRepository) : base(subscriptionService)
        {
            _subscriptionPromotionSeriesBonusRepository = subscriptionPromotionSeriesBonusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _subscriptionPromotionSeriesBonusRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionPromotionSeriesBonusViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubscriptionPromotionSeriesBonus")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _subscriptionPromotionSeriesBonusRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SubscriptionPromotionSeriesBonusViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionPromotionSeriesBonusViewModel subscriptionPromotionSeriesBonusVm)
        {
            if (subscriptionPromotionSeriesBonusVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<SubscriptionPromotionSeriesBonus>(subscriptionPromotionSeriesBonusVm);
            newItem.SetCreation(UserName);
            _subscriptionPromotionSeriesBonusRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<SubscriptionPromotionSeriesBonusViewModel>(newItem);

            return CreatedAtRoute("GetSubscriptionPromotionSeriesBonus", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubscriptionPromotionSeriesBonusViewModel subscriptionPromotionSeriesBonusVm)
        {
            if (subscriptionPromotionSeriesBonusVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _subscriptionPromotionSeriesBonusRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(subscriptionPromotionSeriesBonusVm, dbItem);
            dbItem.SetModification(UserName);
            _subscriptionPromotionSeriesBonusRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubscriptionPromotionSeriesBonusViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _subscriptionPromotionSeriesBonusRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SubscriptionPromotionSeriesBonusViewModel>(dbItem);
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
            var model = await _subscriptionPromotionSeriesBonusRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _subscriptionPromotionSeriesBonusRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("BySubscriptionPromotionSeries/{subscriptionPromotionSeriesId}")]
        public async Task<IActionResult> GetBySubscriptionPromotionSeries(int subscriptionPromotionSeriesId)
        {
            var items = await _subscriptionPromotionSeriesBonusRepository
                .All.Where(x => x.SubscriptionPromotionSeriesId == subscriptionPromotionSeriesId)
                .ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionPromotionSeriesBonusViewModel>>(items);
            return Ok(results);
        }

    }
}

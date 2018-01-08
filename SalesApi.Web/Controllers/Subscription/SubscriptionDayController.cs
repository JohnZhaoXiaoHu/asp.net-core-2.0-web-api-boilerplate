using System;
using System.Collections.Generic;
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
    public class SubscriptionDayController : SubscriptionController<SubscriptionDayController>
    {
        private readonly ISubscriptionDayRepository _subscriptionDayRepository;
        private readonly ISubscriptionDayService _subscriptionDayService;

        public SubscriptionDayController(ISubscriptionService<SubscriptionDayController> subscriptionService,
            ISubscriptionDayRepository subscriptionDayRepository,
            ISubscriptionDayService subscriptionDayService) : base(subscriptionService)
        {
            _subscriptionDayRepository = subscriptionDayRepository;
            _subscriptionDayService = subscriptionDayService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _subscriptionDayRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionDayViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubscriptionDay")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _subscriptionDayRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SubscriptionDayViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionDayViewModel subscriptionDayVm)
        {
            if (subscriptionDayVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<SubscriptionDay>(subscriptionDayVm);
            newItem.SetCreation(UserName);
            _subscriptionDayRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<SubscriptionDayViewModel>(newItem);

            return CreatedAtRoute("GetSubscriptionDay", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubscriptionDayViewModel subscriptionDayVm)
        {
            if (subscriptionDayVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _subscriptionDayRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(subscriptionDayVm, dbItem);
            dbItem.SetModification(UserName);
            _subscriptionDayRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubscriptionDayViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _subscriptionDayRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SubscriptionDayViewModel>(dbItem);
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
            var model = await _subscriptionDayRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _subscriptionDayRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("Tomorrow")]
        public IActionResult GetTomorrow()
        {
            return Ok(Tomorrow);
        }

        [HttpGet]
        [Route("ByDate/{date?}")]
        public async Task<IActionResult> GetByDate(DateTime? date = null)
        {
            var dateStr = GetDateString(date);
            var item = await _subscriptionDayRepository.GetSingleAsync(x => x.Date == dateStr);
            if (item == null)
            {
                return NoContent();
            }
            var result = Mapper.Map<SubscriptionDayViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        [Route("Initialize")]
        public async Task<IActionResult> Initialize()
        {
            await _subscriptionDayService.Initialzie(Tomorrow, UserName);
            await UnitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Infrastructure.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Collective;
using SalesApi.Repositories.Collective;
using SalesApi.ViewModels.Collective;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Collective
{
    [Route("api/sales/[controller]")]
    public class CollectiveOrderController : SalesController<CollectiveOrderController>
    {
        private readonly ICollectiveOrderRepository _collectiveOrderRepository;

        public CollectiveOrderController(ICoreService<CollectiveOrderController> coreService,
            ICollectiveOrderRepository collectiveOrderRepository) : base(coreService)
        {
            _collectiveOrderRepository = collectiveOrderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _collectiveOrderRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<CollectiveOrderViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetCollectiveOrder")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _collectiveOrderRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<CollectiveOrderViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CollectiveOrderViewModel collectiveOrderVm)
        {
            if (collectiveOrderVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<CollectiveOrder>(collectiveOrderVm);
            newItem.SetCreation(UserName);
            _collectiveOrderRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<CollectiveOrderViewModel>(newItem);

            return CreatedAtRoute("GetCollectiveOrder", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CollectiveOrderViewModel collectiveOrderVm)
        {
            if (collectiveOrderVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _collectiveOrderRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(collectiveOrderVm, dbItem);
            dbItem.SetModification(UserName);
            _collectiveOrderRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CollectiveOrderViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _collectiveOrderRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<CollectiveOrderViewModel>(dbItem);
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
            var model = await _collectiveOrderRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _collectiveOrderRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("ByDateAndCollectiveCustomer/{collectiveCustomerId}/{date?}")]
        public async Task<IActionResult> GetByDateAndCollectiveCustomer(int collectiveCustomerId, DateTime? date = null)
        {
            var dateStr = GetDateString(date);
            var items = await _collectiveOrderRepository.All.Where(x => x.CollectiveCustomerId == collectiveCustomerId && x.Date == dateStr).ToListAsync();
            var results = Mapper.Map<IEnumerable<CollectiveOrderViewModel>>(items);
            return Ok(results);
        }

        [HttpPut("SaveOrder/{collectiveProductSnapshotId}/{collectiveCustomerId}/{date}/{ordered}/{gift}")]
        public async Task<IActionResult> SaveOrder(int collectiveProductSnapshotId, int collectiveCustomerId, DateTime date, int ordered, int gift)
        {
            var dateStr = GetDateString(date);
            var collectiveOrder = await _collectiveOrderRepository.GetSingleAsync(x =>
                x.CollectiveProductSnapshotId == collectiveProductSnapshotId && x.CollectiveCustomerId == collectiveCustomerId && x.Date == dateStr);
            if (collectiveOrder == null)
            {
                collectiveOrder = new CollectiveOrder
                {
                    CollectiveProductSnapshotId = collectiveProductSnapshotId,
                    CollectiveCustomerId = collectiveCustomerId,
                    Date = dateStr,
                    Ordered = ordered,
                    Gift = gift
                };
                collectiveOrder.SetCreation(UserName);
                _collectiveOrderRepository.Add(collectiveOrder);
            }
            else
            {
                collectiveOrder.Ordered = ordered;
                collectiveOrder.Gift = gift;
                collectiveOrder.SetModification(UserName);
                _collectiveOrderRepository.Update(collectiveOrder);
            }
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpGet]
        [Route("ByCollectiveCustomereAndDateRange/{collectiveCustomerId}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetByCollectiveCustomereAndDateRange(int collectiveCustomerId, DateTime startDate, DateTime endDate)
        {
            var startDateStr = GetDateString(startDate);
            var endDateStr = GetDateString(endDate);
            var items = await _collectiveOrderRepository.AllIncluding(x => x.CollectiveProductSnapshot)
                .Where(x => x.CollectiveCustomerId == collectiveCustomerId && string.Compare(x.Date, startDateStr, StringComparison.Ordinal) >= 0 && string.Compare(x.Date, endDateStr, StringComparison.Ordinal) <= 0)
                .ToListAsync();
            var results = Mapper.Map<IEnumerable<CollectiveOrderViewModel>>(items);
            return Ok(results);
        }

    }
}

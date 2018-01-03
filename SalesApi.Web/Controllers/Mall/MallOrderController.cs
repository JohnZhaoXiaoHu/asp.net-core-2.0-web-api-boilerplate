using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SalesApi.Models.Mall;
using SalesApi.Repositories.Mall;
using SalesApi.Services.Mall;
using SalesApi.ViewModels.Mall;
using SalesApi.Web.Controllers.Bases;
using SharedSettings.Tools;

namespace SalesApi.Web.Controllers.Mall
{
    [Route("api/sales/[controller]")]
    public class MallOrderController : MallController<MallOrderController>
    {
        private readonly IMallOrderRepository _mallOrderRepository;

        public MallOrderController(IMallService<MallOrderController> service,
            IMallOrderRepository mallOrderRepository) : base(service)
        {
            _mallOrderRepository = mallOrderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _mallOrderRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<MallOrderViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetMallOrder")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _mallOrderRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<MallOrderViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MallOrderViewModel mallOrderVm)
        {
            if (mallOrderVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<MallOrder>(mallOrderVm);
            newItem.SetCreation(UserName);
            _mallOrderRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<MallOrderViewModel>(newItem);

            return CreatedAtRoute("GetMallOrder", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MallOrderViewModel mallOrderVm)
        {
            if (mallOrderVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _mallOrderRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(mallOrderVm, dbItem);
            dbItem.SetModification(UserName);
            _mallOrderRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<MallOrderViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _mallOrderRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<MallOrderViewModel>(dbItem);
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
            var model = await _mallOrderRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _mallOrderRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("ByDateAndMallCustomer/{mallCustomerId}/{date?}")]
        public async Task<IActionResult> GetByDateAndMallCustomer(int mallCustomerId, DateTime? date = null)
        {
            var dateStr = GetDateString(date);
            var items = await _mallOrderRepository.All.Where(x => x.MallCustomerId == mallCustomerId && x.Date == dateStr).ToListAsync();
            var results = Mapper.Map<IEnumerable<MallOrderViewModel>>(items);
            return Ok(results);
        }

        [HttpPut("SaveOrder/{mallProductSnapshotId}/{mallCustomerId}/{date}/{ordered}/{gift}/{price}")]
        public async Task<IActionResult> SaveOrder(int mallProductSnapshotId, int mallCustomerId, DateTime date, int ordered, int gift, decimal price)
        {
            var dateStr = GetDateString(date);
            var mallOrder = await _mallOrderRepository.GetSingleAsync(x =>
                x.MallProductSnapshotId == mallProductSnapshotId && x.MallCustomerId == mallCustomerId && x.Date == dateStr);
            if (mallOrder == null)
            {
                mallOrder = new MallOrder
                {
                    MallProductSnapshotId = mallProductSnapshotId,
                    MallCustomerId = mallCustomerId,
                    Date = dateStr,
                    Ordered = ordered,
                    Gift = gift,
                    Price = price
                };
                mallOrder.SetCreation(UserName);
                _mallOrderRepository.Add(mallOrder);
            }
            else
            {
                mallOrder.Ordered = ordered;
                mallOrder.Gift = gift;
                mallOrder.Price = price;
                mallOrder.SetModification(UserName);
                _mallOrderRepository.Update(mallOrder);
            }
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpGet]
        [Route("ByMallCustomerAndDateRange/{mallCustomerId}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetByMallCustomerAndDateRange(int mallCustomerId, DateTime startDate, DateTime endDate)
        {
            var startDateStr = GetDateString(startDate);
            var endDateStr = GetDateString(endDate);
            var items = await _mallOrderRepository.AllIncluding(x => x.MallProductSnapshot)
                .Where(x => x.MallCustomerId == mallCustomerId && string.Compare(x.Date, startDateStr, StringComparison.Ordinal) >= 0 && string.Compare(x.Date, endDateStr, StringComparison.Ordinal) <= 0)
                .ToListAsync();
            var results = Mapper.Map<IEnumerable<MallOrderViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("SetPrice/{id}", Name = "GetMallOrderSetPrice")]
        public async Task<IActionResult> GetMallOrderSetPrice(int id)
        {
            var item = await _mallOrderRepository.GetSingleAsync(x => x.Id == id, x => x.MallProductSnapshot);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<MallOrderSetPriceViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        [Route("SetPrice")]
        public async Task<IActionResult> SetPriceAdd([FromBody] MallOrderSetPriceViewModel mallOrderVm)
        {
            if (mallOrderVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mallDay = await MallDayRepository.GetSingleAsync(x => x.Date == mallOrderVm.Date, x => x.MallProductSnapshots);
            if (mallDay == null || !mallDay.Initialized)
            {
                throw new Exception("该日期还未初始化");
            }
            var productSnapshot = mallDay.MallProductSnapshots.SingleOrDefault(x =>
                x.ProductForMallId == mallOrderVm.ProductForMallId);
            if (productSnapshot == null)
            {
                throw new Exception($"该产品在{mallDay.Date}不可用");
            }
            var newItem = Mapper.Map<MallOrder>(mallOrderVm);
            newItem.MallProductSnapshotId = productSnapshot.Id;
            newItem.SetCreation(UserName, "区间新增价格");
            _mallOrderRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<MallOrderSetPriceViewModel>(newItem);

            return CreatedAtRoute("GetMallOrderSetPrice", new { id = vm.Id }, vm);
        }

        [HttpPut]
        [Route("SetPrice/{id}/{price}")]
        public async Task<IActionResult> SetPriceEdit(int id, decimal price)
        {
            var dbItem = await _mallOrderRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            dbItem.Price = price;
            dbItem.SetModification(UserName, "区间修改价格");
            _mallOrderRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPost]
        [Route("PeriodSetPrice")]
        public async Task<IActionResult> PeriodSetPrice([FromBody]JObject jt)
        {
            var customerIds = jt["customerIds"].ToObject<List<int>>();
            var start = jt["start"].ToObject<DateTime>();
            var end = jt["end"].ToObject<DateTime>();
            var productForMallId = jt["productForMallId"].ToObject<int>();
            var price = jt["price"].ToObject<decimal>();
            var startStr = GetDateString(start);
            var endStr = GetDateString(end);
            var orders = await _mallOrderRepository.All.Where(x =>
                    customerIds.Contains(x.MallCustomerId) && string.CompareOrdinal(startStr, x.Date) <= 0 &&
                    string.CompareOrdinal(endStr, x.Date) >= 0 && x.MallProductSnapshot.ProductForMallId == productForMallId)
                .ToListAsync();
            if (orders.Any())
            {
                foreach (var order in orders)
                {
                    order.Price = price;
                    order.SetModification(UserName, "修改价格");
                    _mallOrderRepository.Update(order);
                }
                if (!await UnitOfWork.SaveAsync())
                {
                    return StatusCode(500, "保存时出错");
                }
            }
            return NoContent();
        }
    }
}

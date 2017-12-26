using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Infrastructure.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.County;
using SalesApi.Repositories.County;
using SalesApi.Services.County;
using SalesApi.ViewModels.County;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.County
{
    [Route("api/sales/[controller]")]
    public class CountyOrderController : CountyController<CountyOrderController>
    {
        private readonly ICountyOrderRepository _countyOrderRepository;

        public CountyOrderController(ICountyService<CountyOrderController> service,
            ICountyOrderRepository countyOrderRepository) : base(service)
        {
            _countyOrderRepository = countyOrderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _countyOrderRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyOrderViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetCountyOrder")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _countyOrderRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<CountyOrderViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CountyOrderViewModel countyOrderVm)
        {
            if (countyOrderVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<CountyOrder>(countyOrderVm);
            newItem.SetCreation(UserName);
            _countyOrderRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<CountyOrderViewModel>(newItem);

            return CreatedAtRoute("GetCountyOrder", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CountyOrderViewModel countyOrderVm)
        {
            if (countyOrderVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _countyOrderRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(countyOrderVm, dbItem);
            dbItem.SetModification(UserName);
            _countyOrderRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CountyOrderViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _countyOrderRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<CountyOrderViewModel>(dbItem);
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
            var model = await _countyOrderRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _countyOrderRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("ByDateAndCountyAgent/{countyAgentId}/{date?}")]
        public async Task<IActionResult> GetByDateAndCountyAgent(int countyAgentId, DateTime? date = null)
        {
            var dateStr = GetDateString(date);
            var items = await _countyOrderRepository.All.Where(x => x.CountyAgentId == countyAgentId && x.Date == dateStr).ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyOrderViewModel>>(items);
            return Ok(results);
        }

        [HttpPut("SaveOrder/{countyProductSnapshotId}/{countyAgentId}/{date}/{ordered}/{gift}/{price}")]
        public async Task<IActionResult> SaveOrder(int countyProductSnapshotId, int countyAgentId, DateTime date, int ordered, int gift, decimal price)
        {
            var dateStr = GetDateString(date);
            var countyOrder = await _countyOrderRepository.GetSingleAsync(x =>
                x.CountyProductSnapshotId == countyProductSnapshotId && x.CountyAgentId == countyAgentId && x.Date == dateStr);
            if (countyOrder == null)
            {
                countyOrder = new CountyOrder
                {
                    CountyProductSnapshotId = countyProductSnapshotId,
                    CountyAgentId = countyAgentId,
                    Date = dateStr,
                    Ordered = ordered,
                    Gift = gift,
                    Price = price
                };
                countyOrder.SetCreation(UserName);
                _countyOrderRepository.Add(countyOrder);
            }
            else
            {
                countyOrder.Ordered = ordered;
                countyOrder.Gift = gift;
                countyOrder.Price = price;
                countyOrder.SetModification(UserName);
                _countyOrderRepository.Update(countyOrder);
            }
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpGet]
        [Route("ByCountyAgentAndDateRange/{countyAgentId}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetByCountyAgentAndDateRange(int countyAgentId, DateTime startDate, DateTime endDate)
        {
            var startDateStr = GetDateString(startDate);
            var endDateStr = GetDateString(endDate);
            var items = await _countyOrderRepository.AllIncluding(x => x.CountyProductSnapshot)
                .Where(x => x.CountyAgentId == countyAgentId && string.Compare(x.Date, startDateStr, StringComparison.Ordinal) >= 0 && string.Compare(x.Date, endDateStr, StringComparison.Ordinal) <= 0)
                .ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyOrderViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("SetPrice/{id}", Name = "GetCountyOrderSetPrice")]
        public async Task<IActionResult> GetCountyOrderSetPrice(int id)
        {
            var item = await _countyOrderRepository.GetSingleAsync(x => x.Id == id, x => x.CountyProductSnapshot);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<CountyOrderSetPriceViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        [Route("SetPrice")]
        public async Task<IActionResult> SetPriceAdd([FromBody] CountyOrderSetPriceViewModel countyOrderVm)
        {
            if (countyOrderVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var countyDay = await CountyDayRepository.GetSingleAsync(x => x.Date == countyOrderVm.Date, x => x.CountyProductSnapshots);
            if (countyDay == null || !countyDay.Initialized)
            {
                throw new Exception("该日期还未初始化");
            }
            var productSnapshot = countyDay.CountyProductSnapshots.SingleOrDefault(x =>
                x.ProductForCountyId == countyOrderVm.ProductForCountyId);
            if (productSnapshot == null)
            {
                throw new Exception($"该产品在{countyDay.Date}不可用");
            }
            var newItem = Mapper.Map<CountyOrder>(countyOrderVm);
            newItem.CountyProductSnapshotId = productSnapshot.Id;
            newItem.SetCreation(UserName, "区间新增价格");
            _countyOrderRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<CountyOrderSetPriceViewModel>(newItem);

            return CreatedAtRoute("GetCountyOrderSetPrice", new { id = vm.Id }, vm);
        }

        [HttpPut]
        [Route("SetPrice/{id}/{price}")]
        public async Task<IActionResult> SetPriceEdit(int id, decimal price)
        {
            var dbItem = await _countyOrderRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            dbItem.Price = price;
            dbItem.SetModification(UserName, "区间修改价格");
            _countyOrderRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Infrastructure.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Overall;
using SalesApi.Repositories.Overall;
using SalesApi.ViewModels.Overall;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Overall
{
    [Route("api/sales/[controller]")]
    public class SalesDayController : SalesController<SalesDayController>
    {
        private readonly ISalesDayRepository _salesDayRepository;
        public SalesDayController(ICoreService<SalesDayController> coreService,
            ISalesDayRepository salesDayRepository) : base(coreService)
        {
            _salesDayRepository = salesDayRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _salesDayRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<SalesDayViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSalesDay")]
        public async Task<IActionResult> Get(int id)
        {
             var item = await _salesDayRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SalesDayViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SalesDayViewModel salesDayVm)
        {
            if (salesDayVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<SalesDay>(salesDayVm);
            newItem.SetCreation(UserName);
            _salesDayRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<SalesDayViewModel>(newItem);

            return CreatedAtRoute("GetSalesDay", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SalesDayViewModel salesDayVm)
        {
            if (salesDayVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _salesDayRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(salesDayVm, dbItem);
            dbItem.SetModification(UserName);
            _salesDayRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SalesDayViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _salesDayRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SalesDayViewModel>(dbItem);
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
            var model = await _salesDayRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _salesDayRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }
    }
}

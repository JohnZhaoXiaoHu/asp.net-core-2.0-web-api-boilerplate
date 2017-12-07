using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Retail;
using SalesApi.Repositories.Retail;
using SalesApi.Services.Retail;
using SalesApi.ViewModels.Retail;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Retail
{
    [Route("api/sales/[controller]")]
    public class RetailDayController : RetailController<RetailDayController>
    {
        private readonly IRetailDayRepository _retailDayRepository;
        public RetailDayController(IRetailService<RetailDayController> retailService,
            IRetailDayRepository retailDayRepository) : base(retailService)
        {
            _retailDayRepository = retailDayRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _retailDayRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<RetailDayViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetRetailDay")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _retailDayRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<RetailDayViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RetailDayViewModel retailDayVm)
        {
            if (retailDayVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<RetailDay>(retailDayVm);
            newItem.SetCreation(UserName);
            _retailDayRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<RetailDayViewModel>(newItem);

            return CreatedAtRoute("GetRetailDay", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RetailDayViewModel retailDayVm)
        {
            if (retailDayVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _retailDayRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(retailDayVm, dbItem);
            dbItem.SetModification(UserName);
            _retailDayRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<RetailDayViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _retailDayRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<RetailDayViewModel>(dbItem);
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
            var model = await _retailDayRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _retailDayRepository.Delete(model);
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
            var item = await _retailDayRepository.GetSingleAsync(x => x.Date == dateStr);
            if (item == null)
            {
                return NoContent();
            }
            var result = Mapper.Map<RetailDayViewModel>(item);
            return Ok(result);
        }

    }
}

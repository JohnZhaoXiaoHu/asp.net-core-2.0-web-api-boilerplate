using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
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
    public class CountyDayController : CountyController<CountyDayController>
    {
        private readonly ICountyDayRepository _countyDayRepository;
        private readonly ICountyDayService _countyDayService;

        public CountyDayController(ICountyService<CountyDayController> countyService,
            ICountyDayRepository countyDayRepository,
            ICountyDayService countyDayService) : base(countyService)
        {
            _countyDayRepository = countyDayRepository;
            _countyDayService = countyDayService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _countyDayRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyDayViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetCountyDay")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _countyDayRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<CountyDayViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CountyDayViewModel countyDayVm)
        {
            if (countyDayVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<CountyDay>(countyDayVm);
            newItem.SetCreation(UserName);
            _countyDayRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<CountyDayViewModel>(newItem);

            return CreatedAtRoute("GetCountyDay", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CountyDayViewModel countyDayVm)
        {
            if (countyDayVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _countyDayRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(countyDayVm, dbItem);
            dbItem.SetModification(UserName);
            _countyDayRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CountyDayViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _countyDayRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<CountyDayViewModel>(dbItem);
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
            var model = await _countyDayRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _countyDayRepository.Delete(model);
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
            var item = await _countyDayRepository.GetSingleAsync(x => x.Date == dateStr);
            if (item == null)
            {
                return NoContent();
            }
            var result = Mapper.Map<CountyDayViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        [Route("Initialize")]
        public async Task<IActionResult> Initialize()
        {
            await _countyDayService.Initialzie(Tomorrow, UserName);
            await UnitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}

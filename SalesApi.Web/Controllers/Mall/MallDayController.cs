using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Mall;
using SalesApi.Repositories.Mall;
using SalesApi.Services.Mall;
using SalesApi.ViewModels.Mall;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Mall
{
    [Route("api/sales/[controller]")]
    public class MallDayController : MallController<MallDayController>
    {
        private readonly IMallDayRepository _mallDayRepository;
        private readonly IMallDayService _mallDayService;

        public MallDayController(IMallService<MallDayController> mallService,
            IMallDayRepository mallDayRepository,
            IMallDayService mallDayService) : base(mallService)
        {
            _mallDayRepository = mallDayRepository;
            _mallDayService = mallDayService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _mallDayRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<MallDayViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetMallDay")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _mallDayRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<MallDayViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MallDayViewModel mallDayVm)
        {
            if (mallDayVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<MallDay>(mallDayVm);
            newItem.SetCreation(UserName);
            _mallDayRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<MallDayViewModel>(newItem);

            return CreatedAtRoute("GetMallDay", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MallDayViewModel mallDayVm)
        {
            if (mallDayVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _mallDayRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(mallDayVm, dbItem);
            dbItem.SetModification(UserName);
            _mallDayRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<MallDayViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _mallDayRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<MallDayViewModel>(dbItem);
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
            var model = await _mallDayRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _mallDayRepository.Delete(model);
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
            var item = await _mallDayRepository.GetSingleAsync(x => x.Date == dateStr);
            if (item == null)
            {
                return NoContent();
            }
            var result = Mapper.Map<MallDayViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        [Route("Initialize")]
        public async Task<IActionResult> Initialize()
        {
            await _mallDayService.Initialzie(Tomorrow, UserName);
            await UnitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Collective;
using SalesApi.Repositories.Collective;
using SalesApi.Services.Collective;
using SalesApi.ViewModels.Collective;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Collective
{
    [Route("api/sales/[controller]")]
    public class CollectiveDayController : CollectiveController<CollectiveDayController>
    {
        private readonly ICollectiveDayRepository _collectiveDayRepository;
        private readonly ICollectiveDayService _collectiveDayService;

        public CollectiveDayController(ICollectiveService<CollectiveDayController> collectiveService,
            ICollectiveDayRepository collectiveDayRepository,
            ICollectiveDayService collectiveDayService) : base(collectiveService)
        {
            _collectiveDayRepository = collectiveDayRepository;
            _collectiveDayService = collectiveDayService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _collectiveDayRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<CollectiveDayViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetCollectiveDay")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _collectiveDayRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<CollectiveDayViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CollectiveDayViewModel collectiveDayVm)
        {
            if (collectiveDayVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<CollectiveDay>(collectiveDayVm);
            newItem.SetCreation(UserName);
            _collectiveDayRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<CollectiveDayViewModel>(newItem);

            return CreatedAtRoute("GetCollectiveDay", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CollectiveDayViewModel collectiveDayVm)
        {
            if (collectiveDayVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _collectiveDayRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(collectiveDayVm, dbItem);
            dbItem.SetModification(UserName);
            _collectiveDayRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CollectiveDayViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _collectiveDayRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<CollectiveDayViewModel>(dbItem);
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
            var model = await _collectiveDayRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _collectiveDayRepository.Delete(model);
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
            var item = await _collectiveDayRepository.GetSingleAsync(x => x.Date == dateStr);
            if (item == null)
            {
                return NoContent();
            }
            var result = Mapper.Map<CollectiveDayViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        [Route("Initialize")]
        public async Task<IActionResult> Initialize()
        {
            await _collectiveDayService.Initialzie(Tomorrow, UserName);
            await UnitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}

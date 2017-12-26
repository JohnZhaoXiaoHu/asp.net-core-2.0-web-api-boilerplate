using System.Collections.Generic;
using System.Linq;
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
    public class CountyAgentController : CountyController<CountyAgentController>
    {
        private readonly ICountyAgentRepository _countyAgentRepository;

        public CountyAgentController(ICountyService<CountyAgentController> countyService,
            ICountyAgentRepository countyAgentRepository) : base(countyService)
        {
            _countyAgentRepository = countyAgentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _countyAgentRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyAgentViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetCountyAgent")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _countyAgentRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<CountyAgentViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CountyAgentViewModel countyAgentVm)
        {
            if (countyAgentVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<CountyAgent>(countyAgentVm);
            _countyAgentRepository.SetPinyin(newItem);
            newItem.SetCreation(UserName);
            _countyAgentRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<CountyAgentViewModel>(newItem);

            return CreatedAtRoute("GetCountyAgent", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CountyAgentViewModel countyAgentVm)
        {
            if (countyAgentVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _countyAgentRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(countyAgentVm, dbItem);
            _countyAgentRepository.SetPinyin(dbItem);
            dbItem.SetModification(UserName);
            _countyAgentRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<CountyAgentViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CountyAgentViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _countyAgentRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<CountyAgentViewModel>(dbItem);
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
            var model = await _countyAgentRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _countyAgentRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("ByDeliveryVehicle/{deliveryVehicleId}")]
        public async Task<IActionResult> GetByDeliveryVehicle(int deliveryVehicleId)
        {
            var items = await _countyAgentRepository.All.Where(x => x.SubArea.DeliveryVehicleId == deliveryVehicleId).ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyAgentViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("BySubArea/{subAreaId}")]
        public async Task<IActionResult> GetBySubArea(int subAreaId)
        {
            var items = await _countyAgentRepository.All.Where(x => x.SubAreaId == subAreaId).ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyAgentViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("NotDeleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var items = await _countyAgentRepository.All.Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyAgentViewModel>>(items);
            return Ok(results);
        }

    }
}

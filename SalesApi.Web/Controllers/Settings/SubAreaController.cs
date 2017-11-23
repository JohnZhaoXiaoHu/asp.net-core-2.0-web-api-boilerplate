using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Infrastructure.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Settings;
using SalesApi.Repositories.Settings;
using SalesApi.ViewModels.Settings;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Settings
{
    [Route("api/sales/[controller]")]
    public class SubAreaController : SalesController<SubAreaController>
    {
        private readonly ISubAreaRepository _subAreaRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public SubAreaController(ICoreService<SubAreaController> coreService,
            ISubAreaRepository subAreaRepository,
            IVehicleRepository vehicleRepository) : base(coreService)
        {
            _subAreaRepository = subAreaRepository;
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _subAreaRepository.AllIncluding(x => x.DeliveryVehicle).ToListAsync();
            await _vehicleRepository.All.LoadAsync();
            var results = Mapper.Map<IEnumerable<SubAreaViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubArea")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _subAreaRepository.GetSingleAsync(x => x.Id == id, x => x.DeliveryVehicle);
            await _vehicleRepository.All.Where(x => x.Id == item.DeliveryVehicle.VehicleId).LoadAsync();
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SubAreaViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubAreaAddViewModel subAreaVm)
        {
            if (subAreaVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<SubArea>(subAreaVm);
            newItem.SetCreation(UserName);
            _subAreaRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<SubAreaViewModel>(newItem);

            return CreatedAtRoute("GetSubArea", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubAreaEditViewModel subAreaVm)
        {
            if (subAreaVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _subAreaRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(subAreaVm, dbItem);
            dbItem.SetModification(UserName);
            _subAreaRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubAreaEditViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _subAreaRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SubAreaEditViewModel>(dbItem);
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
            var model = await _subAreaRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _subAreaRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }
    }
}

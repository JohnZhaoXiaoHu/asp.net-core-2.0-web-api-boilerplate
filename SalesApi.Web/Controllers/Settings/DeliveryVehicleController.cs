using System.Collections.Generic;
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
    public class DeliveryVehicleController : SalesController<DeliveryVehicleController>
    {
        private readonly IDeliveryVehicleRepository _deliveryVehicleRepository;
        public DeliveryVehicleController(ICoreService<DeliveryVehicleController> coreService,
            IDeliveryVehicleRepository deliveryVehicleRepository) : base(coreService)
        {
            _deliveryVehicleRepository = deliveryVehicleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _deliveryVehicleRepository.AllIncluding(x => x.Vehicle).ToListAsync();
            var results = Mapper.Map<IEnumerable<DeliveryVehicleViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetDeliveryVehicle")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _deliveryVehicleRepository.GetSingleAsync(x => x.Id == id, x => x.Vehicle);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<DeliveryVehicleViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DeliveryVehicleViewModel deliveryVehicleVm)
        {
            if (deliveryVehicleVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<DeliveryVehicle>(deliveryVehicleVm);
            newItem.SetCreation(UserName);
            _deliveryVehicleRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<DeliveryVehicleViewModel>(newItem);

            return CreatedAtRoute("GetDeliveryVehicle", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DeliveryVehicleViewModel deliveryVehicleVm)
        {
            if (deliveryVehicleVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _deliveryVehicleRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(deliveryVehicleVm, dbItem);
            dbItem.SetModification(UserName);
            _deliveryVehicleRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<DeliveryVehicleViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _deliveryVehicleRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<DeliveryVehicleViewModel>(dbItem);
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
            var model = await _deliveryVehicleRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _deliveryVehicleRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }
    }
}

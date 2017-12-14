using System.Collections.Generic;
using System.Linq;
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
    public class CollectiveCustomerController : CollectiveController<CollectiveCustomerController>
    {
        private readonly ICollectiveCustomerRepository _collectiveCustomerRepository;

        public CollectiveCustomerController(ICollectiveService<CollectiveCustomerController> collectiveService,
            ICollectiveCustomerRepository collectiveCustomerRepository) : base(collectiveService)
        {
            _collectiveCustomerRepository = collectiveCustomerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _collectiveCustomerRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<CollectiveCustomerViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetCollectiveCustomer")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _collectiveCustomerRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<CollectiveCustomerViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CollectiveCustomerViewModel collectiveCustomerVm)
        {
            if (collectiveCustomerVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<CollectiveCustomer>(collectiveCustomerVm);
            _collectiveCustomerRepository.SetPinyin(newItem);
            newItem.SetCreation(UserName);
            _collectiveCustomerRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<CollectiveCustomerViewModel>(newItem);

            return CreatedAtRoute("GetCollectiveCustomer", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CollectiveCustomerViewModel collectiveCustomerVm)
        {
            if (collectiveCustomerVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _collectiveCustomerRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(collectiveCustomerVm, dbItem);
            _collectiveCustomerRepository.SetPinyin(dbItem);
            dbItem.SetModification(UserName);
            _collectiveCustomerRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<CollectiveCustomerViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CollectiveCustomerViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _collectiveCustomerRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<CollectiveCustomerViewModel>(dbItem);
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
            var model = await _collectiveCustomerRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _collectiveCustomerRepository.Delete(model);
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
            var items = await _collectiveCustomerRepository.All.Where(x => x.SubArea.DeliveryVehicleId == deliveryVehicleId).ToListAsync();
            var results = Mapper.Map<IEnumerable<CollectiveCustomerViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("BySubArea/{subAreaId}")]
        public async Task<IActionResult> GetBySubArea(int subAreaId)
        {
            var items = await _collectiveCustomerRepository.All.Where(x => x.SubAreaId == subAreaId).ToListAsync();
            var results = Mapper.Map<IEnumerable<CollectiveCustomerViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("NotDeleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var items = await _collectiveCustomerRepository.All.Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<CollectiveCustomerViewModel>>(items);
            return Ok(results);
        }

    }
}

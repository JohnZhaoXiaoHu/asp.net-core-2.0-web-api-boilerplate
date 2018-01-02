using System.Collections.Generic;
using System.Linq;
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
    public class MallCustomerController : MallController<MallCustomerController>
    {
        private readonly IMallCustomerRepository _mallCustomerRepository;

        public MallCustomerController(IMallService<MallCustomerController> mallService,
            IMallCustomerRepository mallCustomerRepository) : base(mallService)
        {
            _mallCustomerRepository = mallCustomerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _mallCustomerRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<MallCustomerViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetMallCustomer")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _mallCustomerRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<MallCustomerViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MallCustomerViewModel mallCustomerVm)
        {
            if (mallCustomerVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<MallCustomer>(mallCustomerVm);
            _mallCustomerRepository.SetPinyin(newItem);
            newItem.SetCreation(UserName);
            _mallCustomerRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<MallCustomerViewModel>(newItem);

            return CreatedAtRoute("GetMallCustomer", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MallCustomerViewModel mallCustomerVm)
        {
            if (mallCustomerVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _mallCustomerRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(mallCustomerVm, dbItem);
            _mallCustomerRepository.SetPinyin(dbItem);
            dbItem.SetModification(UserName);
            _mallCustomerRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<MallCustomerViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<MallCustomerViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _mallCustomerRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<MallCustomerViewModel>(dbItem);
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
            var model = await _mallCustomerRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _mallCustomerRepository.Delete(model);
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
            var items = await _mallCustomerRepository.All.Where(x => x.SubArea.DeliveryVehicleId == deliveryVehicleId).ToListAsync();
            var results = Mapper.Map<IEnumerable<MallCustomerViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("BySubArea/{subAreaId}")]
        public async Task<IActionResult> GetBySubArea(int subAreaId)
        {
            var items = await _mallCustomerRepository.All.Where(x => x.SubAreaId == subAreaId).ToListAsync();
            var results = Mapper.Map<IEnumerable<MallCustomerViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("NotDeleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var items = await _mallCustomerRepository.All.Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<MallCustomerViewModel>>(items);
            return Ok(results);
        }

    }
}

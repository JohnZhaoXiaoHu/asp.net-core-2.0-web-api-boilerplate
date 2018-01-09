using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Infrastructure.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Subscription;
using SalesApi.Repositories.Subscription;
using SalesApi.ViewModels.Subscription;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Subscription
{
    [Route("api/sales/[controller]")]
    public class MilkmanController : SalesController<MilkmanController>
    {
        private readonly IMilkmanRepository _milkmanRepository;
        public MilkmanController(ICoreService<MilkmanController> coreService,
            IMilkmanRepository milkmanRepository) : base(coreService)
        {
            _milkmanRepository = milkmanRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _milkmanRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<MilkmanViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetMilkman")]
        public async Task<IActionResult> Get(int id)
        {
             var item = await _milkmanRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<MilkmanViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MilkmanViewModel milkmanVm)
        {
            if (milkmanVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<Milkman>(milkmanVm);
            _milkmanRepository.SetPinyin(newItem);
            newItem.SetCreation(UserName);
            _milkmanRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<MilkmanViewModel>(newItem);

            return CreatedAtRoute("GetMilkman", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MilkmanViewModel milkmanVm)
        {
            if (milkmanVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _milkmanRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(milkmanVm, dbItem);
            _milkmanRepository.SetPinyin(dbItem);
            dbItem.SetModification(UserName);
            _milkmanRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<MilkmanViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _milkmanRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<MilkmanViewModel>(dbItem);
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
            var model = await _milkmanRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _milkmanRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("NotDeleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var items = await _milkmanRepository.All.Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<MilkmanViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("ByDeliveryVehicle/{deliveryVehicleId}")]
        public async Task<IActionResult> GetByDeliveryVehicle(int deliveryVehicleId)
        {
            var items = await _milkmanRepository.All.Where(x => x.SubArea.DeliveryVehicleId == deliveryVehicleId).ToListAsync();
            var results = Mapper.Map<IEnumerable<MilkmanViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("BySubArea/{subAreaId}")]
        public async Task<IActionResult> GetBySubArea(int subAreaId)
        {
            var items = await _milkmanRepository.All.Where(x => x.SubAreaId == subAreaId).ToListAsync();
            var results = Mapper.Map<IEnumerable<MilkmanViewModel>>(items);
            return Ok(results);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Infrastructure.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.County;
using SalesApi.Repositories.County;
using SalesApi.ViewModels.County;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.County
{
    [Route("api/sales/[controller]")]
    public class CountyAgentPriceController : SalesController<CountyAgentPriceController>
    {
        private readonly ICountyAgentPriceRepository _countyAgentPriceRepository;
        public CountyAgentPriceController(ICoreService<CountyAgentPriceController> coreService,
            ICountyAgentPriceRepository countyAgentPriceRepository) : base(coreService)
        {
            _countyAgentPriceRepository = countyAgentPriceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _countyAgentPriceRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyAgentPriceViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetCountyAgentPrice")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _countyAgentPriceRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<CountyAgentPriceViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CountyAgentPriceViewModel countyAgentPriceVm)
        {
            if (countyAgentPriceVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<CountyAgentPrice>(countyAgentPriceVm);
            newItem.SetCreation(UserName);
            _countyAgentPriceRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<CountyAgentPriceViewModel>(newItem);

            return CreatedAtRoute("GetCountyAgentPrice", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CountyAgentPriceViewModel countyAgentPriceVm)
        {
            if (countyAgentPriceVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _countyAgentPriceRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(countyAgentPriceVm, dbItem);
            dbItem.SetModification(UserName);
            _countyAgentPriceRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CountyAgentPriceViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _countyAgentPriceRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<CountyAgentPriceViewModel>(dbItem);
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
            var model = await _countyAgentPriceRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _countyAgentPriceRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("ByCountyAgent/{countyAgentId}")]
        public async Task<IActionResult> GetByCountyAgent(int countyAgentId)
        {
            var items = await _countyAgentPriceRepository.All.Where(x => x.CountyAgentId == countyAgentId).ToListAsync();
            var results = Mapper.Map<IEnumerable<CountyAgentPriceViewModel>>(items);
            return Ok(results);
        }

    }
}

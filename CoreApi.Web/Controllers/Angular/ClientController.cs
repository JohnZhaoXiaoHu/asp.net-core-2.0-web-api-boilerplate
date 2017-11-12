using System.Collections.Generic;
using System.Threading.Tasks;
using CoreApi.Models.Angular;
using CoreApi.Repositories.Angular;
using CoreApi.ViewModels.Angular;
using CoreApi.Web.Controllers.Bases;
using Infrastructure.Features.Common;
using Infrastructure.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreApi.Web.Controllers.Angular
{
    [Route("api/[controller]")]
    public class ClientController : BaseController<ClientController>
    {
        private readonly IClientRepository _clientRepository;
        public ClientController(ICoreService<ClientController> coreService,
            IClientRepository clientRepository) : base(coreService)
        {

            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _clientRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<ClientViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetClient")]
        public async Task<IActionResult> Get(int id)
        {
             var item = await _clientRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<ClientViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientCreationViewModel clientVm)
        {
            if (clientVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<Client>(clientVm);
            newItem.SetCreation(UserName);
            _clientRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存客户时出错");
            }

            var vm = Mapper.Map<ClientViewModel>(newItem);

            return CreatedAtRoute("GetClient", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ClientModificationViewModel clientVm)
        {
            if (clientVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _clientRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(clientVm, dbItem);
            dbItem.SetModification(UserName);
            _clientRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存客户时出错");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<ClientModificationViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _clientRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<ClientModificationViewModel>(dbItem);
            patchDoc.ApplyTo(toPatchVm, ModelState);

            TryValidateModel(toPatchVm);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(toPatchVm, dbItem);

            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "更新的时候出错");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _clientRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _clientRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除的时候出错");
            }
            return NoContent();
        }
    }
}

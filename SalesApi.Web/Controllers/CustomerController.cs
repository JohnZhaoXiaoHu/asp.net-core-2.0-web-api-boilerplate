using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Core.Abstractions.Hateoas;
using SalesApi.Core.DomainModels;
using SalesApi.Core.IRepositories;
using SalesApi.Core.Services;
using SalesApi.Shared.Enums;
using SalesApi.ViewModels;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers
{
    [AllowAnonymous]
    [Route("api/sales/[controller]")]
    public class CustomerController : SalesBaseController<CustomerController>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUrlHelper _urlHelper;

        public CustomerController(
            ICoreService<CustomerController> coreService,
            ICustomerRepository customerRepository,
            IUrlHelper urlHelper) : base(coreService)
        {
            _customerRepository = customerRepository;
            this._urlHelper = urlHelper;
        }

        // [HttpGet(Name = "GetAllCustomers")]
        // public async Task<IActionResult> GetAll()
        // {
        //     var items = await _customerRepository.All.ToListAsync();
        //     var results = Mapper.Map<IEnumerable<CustomerViewModel>>(items);
        //     results = results.Select(CreateLinksForCustomer);
        //     var wrapper = new LinkedCollectionResourceWrapperViewModel<CustomerViewModel>(results);
        //     return Ok(CreateLinksForCustomer(wrapper));
        // }

        // [HttpGet]
        // [Route("{id}", Name = "GetCustomer")]
        // public async Task<IActionResult> Get(int id)
        // {
        //     var item = await _customerRepository.GetSingleAsync(id);
        //     if (item == null)
        //     {
        //         return NotFound();
        //     }
        //     var customerVm = Mapper.Map<CustomerViewModel>(item);
        //     var links = CreateLinksForCustomer(id, )
        //     return Ok(CreateLinksForCustomer(customerVm));
        // }

        // [HttpPost]
        // public async Task<IActionResult> Post([FromBody] CustomerViewModel customerVm)
        // {
        //     if (customerVm == null)
        //     {
        //         return BadRequest();
        //     }

        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     var newItem = Mapper.Map<Customer>(customerVm);
        //     _customerRepository.Add(newItem);
        //     if (!await UnitOfWork.SaveAsync())
        //     {
        //         return StatusCode(500, "保存时出错");
        //     }

        //     var vm = Mapper.Map<CustomerViewModel>(newItem);

        //     return CreatedAtRoute("GetCustomer", new { id = vm.Id }, CreateLinksForCustomer(vm));
        // }

        // [HttpPut("{id}", Name = "UpdateCustomer")]
        // public async Task<IActionResult> Put(int id, [FromBody] CustomerViewModel customerVm)
        // {
        //     if (customerVm == null)
        //     {
        //         return BadRequest();
        //     }

        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        //     var dbItem = await _customerRepository.GetSingleAsync(id);
        //     if (dbItem == null)
        //     {
        //         return NotFound();
        //     }
        //     Mapper.Map(customerVm, dbItem);
        //     _customerRepository.Update(dbItem);
        //     if (!await UnitOfWork.SaveAsync())
        //     {
        //         return StatusCode(500, "保存时出错");
        //     }

        //     return NoContent();
        // }

        // [HttpPatch("{id}", Name = "PartiallyUpdateCustomer")]
        // public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CustomerViewModel> patchDoc)
        // {
        //     if (patchDoc == null)
        //     {
        //         return BadRequest();
        //     }
        //     var dbItem = await _customerRepository.GetSingleAsync(id);
        //     if (dbItem == null)
        //     {
        //         return NotFound();
        //     }
        //     var toPatchVm = Mapper.Map<CustomerViewModel>(dbItem);
        //     patchDoc.ApplyTo(toPatchVm, ModelState);

        //     TryValidateModel(toPatchVm);
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     Mapper.Map(toPatchVm, dbItem);

        //     if (!await UnitOfWork.SaveAsync())
        //     {
        //         return StatusCode(500, "更新时出错");
        //     }

        //     return NoContent();
        // }

        [HttpDelete("{id}", Name = "DeleteCustomer")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _customerRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _customerRepository.Delete(model);
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
            var items = await _customerRepository.All.Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<CustomerViewModel>>(items);
            return Ok(results);
        }

        private IEnumerable<LinkViewModel> CreateLinksForCustomer(int id, string fields)
        {
            var links = new List<LinkViewModel>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(
                    new LinkViewModel(_urlHelper.Link("GetCustomer", new { id = id }),
                    "self",
                    "GET"));
            }
            else
            {
                links.Add(
                    new LinkViewModel(_urlHelper.Link("GetCustomer", new { id = id, fields = fields }),
                    "self",
                    "GET"));
            }

            links.Add(
                new LinkViewModel(_urlHelper.Link("DeleteCustomer", new { id = id }),
                "delete_customer",
                "DELETE"));

            return links;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesApi.Shared.Enums;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Core
{
    [Route("api/sales/[controller]")]
    public class EnumController : SalesController<EnumController>
    {
        public EnumController(ICoreService<EnumController> coreService) : base(coreService)
        {

        }

        [HttpGet]
        [Route("SalesType")]
        [AllowAnonymous]
        public IActionResult GetSalesType()
        {
            var types = Enum.GetValues(typeof(SalesType)).OfType<SalesType>().Select(x => new KeyValuePair<string, SalesType>(x.ToString(), x)).ToList();
            return Ok(types);
        }

        [HttpGet]
        [Route("ProductUnit")]
        [AllowAnonymous]
        public IActionResult GetProductUnit()
        {
            var types = Enum.GetValues(typeof(ProductUnit)).OfType<ProductUnit>().Select(x => new KeyValuePair<string, ProductUnit>(x.ToString(), x)).ToList();
            return Ok(types);
        }
    }
}

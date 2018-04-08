using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using SalesApi.Core.Abstractions.Data;
using SalesApi.Core.Services;
using SalesApi.Web.Helpers;

namespace SalesApi.Web.Controllers.Bases
{
    public abstract class SalesBaseController<T> : Controller
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly ILogger<T> Logger;
        protected readonly IMapper Mapper;
        protected readonly ICoreService<T> CoreService;

        protected SalesBaseController(ICoreService<T> coreService)
        {
            CoreService = coreService;
            UnitOfWork = coreService.UnitOfWork;
            Logger = coreService.Logger;
            Mapper = coreService.Mapper;
        }

        #region Current Information

        protected DateTimeOffset Now => DateTime.Now;
        protected string UserName => User.Identity.Name ?? "Anonymous";
        protected DateTimeOffset Today => DateTime.Now.Date;

        #endregion

        #region HTTP Status Codes

        protected UnprocessableEntityObjectResult UnprocessableEntity(ModelStateDictionary modelState)
        {
            return new UnprocessableEntityObjectResult(modelState);
        }

        #endregion
    }
}
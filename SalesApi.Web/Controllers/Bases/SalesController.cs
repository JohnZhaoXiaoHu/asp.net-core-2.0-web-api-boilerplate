using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalesApi.Core.Abstractions.Data;
using SalesApi.Core.Services;

namespace SalesApi.Web.Controllers.Bases
{
    public abstract class SalesController<T> : Controller
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly ILogger<T> Logger;
        protected readonly IMapper Mapper;
        protected readonly ICoreService<T> CoreService;

        protected SalesController(ICoreService<T> coreService)
        {
            CoreService = coreService;
            UnitOfWork = coreService.UnitOfWork;
            Logger = coreService.Logger;
            Mapper = coreService.Mapper;
        }

        #region Current Information

        protected DateTime Now => DateTime.Now;
        protected string UserName => User.Identity.Name ?? "Anonymous";
        protected DateTime Today => DateTime.Now.Date;

        #endregion
    }
}
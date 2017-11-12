using System;
using AutoMapper;
using Infrastructure.Features.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace SalesApi.Web.Controllers.Bases
{
    public abstract class SalesController<T> : Controller
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly ILogger<T> Logger;
        protected readonly IFileProvider FileProvider;
        protected readonly IMapper Mapper;
        protected readonly ICoreService<T> CoreService;

        protected SalesController(ICoreService<T> coreService)
        {
            CoreService = coreService;
            UnitOfWork = coreService.UnitOfWork;
            Logger = coreService.Logger;
            FileProvider = coreService.FileProvider;
            Mapper = coreService.Mapper;
        }

        #region Current Information

        protected DateTime Now => DateTime.Now;
        protected string UserName => User.Identity.Name ?? "Anonymous";

        #endregion

    }
}
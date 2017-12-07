using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Retail;
using SalesApi.Repositories.Retail;
using SalesApi.Services.Retail;

namespace SalesApi.Web.Controllers.Bases
{
    public abstract class RetailController<T>: SalesController<T>
    {
        protected readonly IRetailService<T> RetailService;
        protected readonly IRetailDayRepository RetailDayRepository;

        protected RetailController(IRetailService<T> retailService) : base(retailService)
        {
            RetailService = retailService;
            RetailDayRepository = RetailService.RetailDayRepository;
        }

        [NonAction]
        protected async Task<bool> HasRetailDayBeenInitialized(DateTime? date = null)
        {
            var dateStr = !date.HasValue ? Now.AddDays(1).Date.ToString("yyyy-MM-dd") : date.Value.ToString("yyyy-MM-dd");
            var item = await RetailDayRepository.GetSingleAsync(x => x.Date == dateStr);
            return item != null && item.Initialized;
        }

        [NonAction]
        protected async Task<RetailDay> GetLatestInitializedRetailDay()
        {
            var item = await RetailDayRepository.All.OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            return item;
        }
    }
}

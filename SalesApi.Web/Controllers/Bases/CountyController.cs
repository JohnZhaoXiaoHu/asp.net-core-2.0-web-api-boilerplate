using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.County;
using SalesApi.Repositories.County;
using SalesApi.Services.County;

namespace SalesApi.Web.Controllers.Bases
{
    public abstract class CountyController<T>: SalesController<T>
    {
        protected readonly ICountyService<T> CountyService;
        protected readonly ICountyDayRepository CountyDayRepository;

        protected CountyController(ICountyService<T> collectiveService) : base(collectiveService)
        {
            CountyService = collectiveService;
            CountyDayRepository = CountyService.CountyDayRepository;
        }

        [NonAction]
        protected async Task<bool> HasCountyDayBeenInitialized(DateTime? date = null)
        {
            var dateStr = !date.HasValue ? Now.AddDays(1).Date.ToString("yyyy-MM-dd") : date.Value.ToString("yyyy-MM-dd");
            var item = await CountyDayRepository.GetSingleAsync(x => x.Date == dateStr);
            return item != null && item.Initialized;
        }

        [NonAction]
        protected async Task<CountyDay> GetLatestInitializedCountyDay()
        {
            var item = await CountyDayRepository.All.OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            return item;
        }
    }
}

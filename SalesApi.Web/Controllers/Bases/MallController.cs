using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Mall;
using SalesApi.Repositories.Mall;
using SalesApi.Services.Mall;

namespace SalesApi.Web.Controllers.Bases
{
    public abstract class MallController<T>: SalesController<T>
    {
        protected readonly IMallService<T> MallService;
        protected readonly IMallDayRepository MallDayRepository;

        protected MallController(IMallService<T> mallService) : base(mallService)
        {
            MallService = mallService;
            MallDayRepository = MallService.MallDayRepository;
        }

        [NonAction]
        protected async Task<bool> HasMallDayBeenInitialized(DateTime? date = null)
        {
            var dateStr = !date.HasValue ? Now.AddDays(1).Date.ToString("yyyy-MM-dd") : date.Value.ToString("yyyy-MM-dd");
            var item = await MallDayRepository.GetSingleAsync(x => x.Date == dateStr);
            return item != null && item.Initialized;
        }

        [NonAction]
        protected async Task<MallDay> GetLatestInitializedMallDay()
        {
            var item = await MallDayRepository.All.OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            return item;
        }
    }
}

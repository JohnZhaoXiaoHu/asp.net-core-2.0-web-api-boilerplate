using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Collective;
using SalesApi.Repositories.Collective;
using SalesApi.Services.Collective;

namespace SalesApi.Web.Controllers.Bases
{
    public abstract class CollectiveController<T>: SalesController<T>
    {
        protected readonly ICollectiveService<T> CollectiveService;
        protected readonly ICollectiveDayRepository CollectiveDayRepository;

        protected CollectiveController(ICollectiveService<T> collectiveService) : base(collectiveService)
        {
            CollectiveService = collectiveService;
            CollectiveDayRepository = CollectiveService.CollectiveDayRepository;
        }

        [NonAction]
        protected async Task<bool> HasCollectiveDayBeenInitialized(DateTime? date = null)
        {
            var dateStr = !date.HasValue ? Now.AddDays(1).Date.ToString("yyyy-MM-dd") : date.Value.ToString("yyyy-MM-dd");
            var item = await CollectiveDayRepository.GetSingleAsync(x => x.Date == dateStr);
            return item != null && item.Initialized;
        }

        [NonAction]
        protected async Task<CollectiveDay> GetLatestInitializedCollectiveDay()
        {
            var item = await CollectiveDayRepository.All.OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            return item;
        }
    }
}

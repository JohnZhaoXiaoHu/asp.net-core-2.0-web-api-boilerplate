using System.Collections.Generic;
using System.Linq;
using Infrastructure.Features.Data;
using SalesApi.Models.County;

namespace SalesApi.Repositories.County
{
    public interface ICountyPromotionEventRepository: IEntityBaseRepository<CountyPromotionEvent>
    {
        IEnumerable<CountyPromotionEvent> GenerateEvents(CountyPromotionSeries rps);
    }

    public class CountyPromotionEventRepository: EntityBaseRepository<CountyPromotionEvent>, ICountyPromotionEventRepository
    {
        public CountyPromotionEventRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public IEnumerable<CountyPromotionEvent> GenerateEvents(CountyPromotionSeries rps)
        {
            for (var d = rps.StartDate; d <= rps.EndDate; d = d.AddDays(1))
            {
                yield return new CountyPromotionEvent
                {
                    CreateTime = rps.CreateTime,
                    CreateUser = rps.CreateUser,
                    UpdateTime = rps.UpdateTime,
                    UpdateUser = rps.UpdateUser,
                    Date = d,
                    Deleted = false,
                    LastAction = "add",
                    Name = rps.Name,
                    Order = d.Subtract(rps.StartDate).Days,
                    CountyPromotionSeriesId = rps.Id,
                    ProductForCountyId = rps.ProductForCountyId,
                    PurchaseBase = rps.PurchaseBase,
                    CountyPromotionEventBonuses = rps.CountyPromotionSeriesBonuses.Select(x => new CountyPromotionEventBonus
                    {
                        ProductForCountyId = x.ProductForCountyId,
                        BonusCount = x.BonusCount,
                        LastAction = "add",
                        CreateTime = rps.CreateTime,
                        CreateUser = rps.CreateUser,
                        UpdateTime = rps.UpdateTime,
                        UpdateUser = rps.UpdateUser
                    }).ToList()
                };
            }
        }
    }
}

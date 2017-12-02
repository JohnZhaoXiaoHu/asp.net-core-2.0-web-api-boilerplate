using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Features.Data;
using SalesApi.Models.Retail;

namespace SalesApi.Repositories.Retail
{
    public interface IRetailPromotionEventRepository : IEntityBaseRepository<RetailPromotionEvent>
    {
        IEnumerable<RetailPromotionEvent> GenerateEvents(RetailPromotionSeries rps);
    }

    public class RetailPromotionEventRepository : EntityBaseRepository<RetailPromotionEvent>, IRetailPromotionEventRepository
    {
        public RetailPromotionEventRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<RetailPromotionEvent> GenerateEvents(RetailPromotionSeries rps)
        {
            for (var d = rps.StartDate; d <= rps.EndDate; d = d.AddDays(1))
            {
                yield return new RetailPromotionEvent
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
                    RetailPromotionSeriesId = rps.Id,
                    ProductForRetailId = rps.ProductForRetailId,
                    PurchaseBase = rps.PurchaseBase,
                    RetailPromotionEventBonuses = rps.RetailPromotionSeriesBonuses.Select(x => new RetailPromotionEventBonus
                    {
                        ProductForRetailId = x.ProductForRetailId,
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

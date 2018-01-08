using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Features.Data;
using SalesApi.Models.Subscription;

namespace SalesApi.Repositories.Subscription
{
    public interface ISubscriptionPromotionEventRepository : IEntityBaseRepository<SubscriptionPromotionEvent>
    {
        IEnumerable<SubscriptionPromotionEvent> GenerateEvents(SubscriptionPromotionSeries rps);
    }

    public class SubscriptionPromotionEventRepository : EntityBaseRepository<SubscriptionPromotionEvent>, ISubscriptionPromotionEventRepository
    {
        public SubscriptionPromotionEventRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<SubscriptionPromotionEvent> GenerateEvents(SubscriptionPromotionSeries rps)
        {
            for (var d = rps.StartDate; d <= rps.EndDate; d = d.AddDays(1))
            {
                yield return new SubscriptionPromotionEvent
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
                    SubscriptionPromotionSeriesId = rps.Id,
                    ProductForSubscriptionId = rps.ProductForSubscriptionId,
                    PurchaseBase = rps.PurchaseBase,
                    SubscriptionPromotionEventBonuses = rps.SubscriptionPromotionSeriesBonuses.Select(x => new SubscriptionPromotionEventBonus
                    {
                        ProductForSubscriptionId = x.ProductForSubscriptionId,
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

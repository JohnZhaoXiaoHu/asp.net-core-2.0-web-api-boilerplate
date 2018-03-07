using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Data;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Subscription.Order;

namespace SalesApi.Repositories.Subscription.Order
{
    public interface ISubscriptionOrderRepository : IEntityBaseRepository<SubscriptionOrder>
    {
        Task<List<SubscriptionOrder>> GetByCreateTimeAsync(DateTime createTime);
    }

    public class SubscriptionOrderRepository : EntityBaseRepository<SubscriptionOrder>, ISubscriptionOrderRepository
    {
        public SubscriptionOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<SubscriptionOrder>> GetByCreateTimeAsync(DateTime createTime)
        {
            var orders = await Context.Set<SubscriptionOrder>()
                .Include(x => x.Milkman)
                .Include(x => x.SubscriptionProductSnapshot)
                .Include(x => x.SubscriptionMonthPromotion)
                    .ThenInclude(y => y.SubscriptionMonthPromotionBonuses)
                        .ThenInclude(z => z.SubscriptionMonthPromotionBonusDates)
                .Include(x => x.SubscriptionMonthPromotion)
                    .ThenInclude(y => y.SubscriptionMonthPromotionBonuses)
                        .ThenInclude(z => z.ProductForSubscription)
                            .ThenInclude(_ => _.Product)
                .Include(x => x.SubscriptionMonthPromotion)
                    .ThenInclude(y => y.ProductForSubscription)
                        .ThenInclude(z => z.Product)
                .Include(x => x.SubscriptionOrderDates)
                .Include(x => x.SubscriptionOrderBonusDates)
                    .ThenInclude(y => y.SubscriptionMonthPromotionBonusDate)
                .Include(x => x.SubscriptionOrderModifiedBonusDates)
                    .ThenInclude(y => y.SubscriptionProductSnapshot)
                .Where(x => x.CreateTime == createTime)
                .ToListAsync();
            return orders;
        }
    }
}

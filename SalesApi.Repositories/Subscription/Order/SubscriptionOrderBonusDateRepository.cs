using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Data;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Subscription.Order;
using SalesApi.Models.Subscription.Promotion;

namespace SalesApi.Repositories.Subscription.Order
{
    public interface ISubscriptionOrderBonusDateRepository: IEntityBaseRepository<SubscriptionOrderBonusDate> {
        Task<List<SubscriptionMonthPromotionBonusDate>> GetWithGrandPromotionInformation(int milkmanId, IEnumerable<DateTime> dates);
    }

    public class SubscriptionOrderBonusDateRepository: EntityBaseRepository<SubscriptionOrderBonusDate>, ISubscriptionOrderBonusDateRepository
    {
        public SubscriptionOrderBonusDateRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<SubscriptionMonthPromotionBonusDate>> GetWithGrandPromotionInformation(int milkmanId, IEnumerable<DateTime> dates)
        {
            var result = await Context.Set<SubscriptionOrderBonusDate>()
                .Include(x => x.SubscriptionMonthPromotionBonusDate)
                .ThenInclude(x => x.SubscriptionMonthPromotionBonus).Select(x=>x.SubscriptionMonthPromotionBonusDate).ToListAsync();
            return result;
        }
    }
}

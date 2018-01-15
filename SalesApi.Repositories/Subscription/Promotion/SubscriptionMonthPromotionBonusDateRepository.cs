using Infrastructure.Features.Data;
using SalesApi.Models.Subscription.Promotion;

namespace SalesApi.Repositories.Subscription.Promotion
{
    public interface
        ISubscriptionMonthPromotionBonusDateRepository : IEntityBaseRepository<SubscriptionMonthPromotionBonusDate>
    {
        
    }

    public class SubscriptionMonthPromotionBonusDateRepository: EntityBaseRepository<SubscriptionMonthPromotionBonusDate>, ISubscriptionMonthPromotionBonusDateRepository
    {
        public SubscriptionMonthPromotionBonusDateRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

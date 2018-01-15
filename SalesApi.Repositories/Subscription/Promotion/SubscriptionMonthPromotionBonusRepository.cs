using Infrastructure.Features.Data;
using SalesApi.Models.Subscription.Promotion;

namespace SalesApi.Repositories.Subscription.Promotion
{
    public interface ISubscriptionMonthPromotionBonusRepository : IEntityBaseRepository<SubscriptionMonthPromotionBonus>
    {
        
    }

    public class SubscriptionMonthPromotionBonusRepository: EntityBaseRepository<SubscriptionMonthPromotionBonus>, ISubscriptionMonthPromotionBonusRepository
    {
        public SubscriptionMonthPromotionBonusRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

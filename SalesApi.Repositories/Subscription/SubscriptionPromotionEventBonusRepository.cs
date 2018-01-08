using Infrastructure.Features.Data;
using SalesApi.Models.Subscription;

namespace SalesApi.Repositories.Subscription
{
    public interface ISubscriptionPromotionEventBonusRepository : IEntityBaseRepository<SubscriptionPromotionEventBonus>
    {

    }

    public class SubscriptionPromotionEventBonusRepository : EntityBaseRepository<SubscriptionPromotionEventBonus>, ISubscriptionPromotionEventBonusRepository
    {
        public SubscriptionPromotionEventBonusRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

using Infrastructure.Features.Data;
using SalesApi.Models.Subscription.Promotion;

namespace SalesApi.Repositories.Subscription.Promotion
{
    public interface ISubscriptionMonthPromotionRepository : IEntityBaseRepository<SubscriptionMonthPromotion>
    {
        
    }

    public class SubscriptionMonthPromotionRepository: EntityBaseRepository<SubscriptionMonthPromotion>, ISubscriptionMonthPromotionRepository
    {
        public SubscriptionMonthPromotionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

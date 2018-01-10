using Infrastructure.Features.Data;
using SalesApi.Models.Subscription.Promotion;

namespace SalesApi.Repositories.Subscription.Promotion
{
    public interface ISubscriptionPromotionRepository : IEntityBaseRepository<SubscriptionPromotion> { }

    public class SubscriptionPromotionRepository : EntityBaseRepository<SubscriptionPromotion>, ISubscriptionPromotionRepository
    {
        public SubscriptionPromotionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

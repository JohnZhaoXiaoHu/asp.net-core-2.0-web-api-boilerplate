using Infrastructure.Features.Data;
using SalesApi.Models.Subscription.Promotion;

namespace SalesApi.Repositories.Subscription.Promotion
{
    public interface ISubscriptionPromotionMonthBonusRepository : IEntityBaseRepository<SubscriptionPromotionMonthBonus> { }

    public class SubscriptionPromotionMonthBonusRepository : EntityBaseRepository<SubscriptionPromotionMonthBonus>, ISubscriptionPromotionMonthBonusRepository
    {
        public SubscriptionPromotionMonthBonusRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

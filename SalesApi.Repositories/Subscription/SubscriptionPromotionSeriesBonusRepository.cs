using Infrastructure.Features.Data;
using SalesApi.Models.Subscription;

namespace SalesApi.Repositories.Subscription
{
    public interface ISubscriptionPromotionSeriesBonusRepository : IEntityBaseRepository<SubscriptionPromotionSeriesBonus>
    {

    }

    public class SubscriptionPromotionSeriesBonusRepository : EntityBaseRepository<SubscriptionPromotionSeriesBonus>, ISubscriptionPromotionSeriesBonusRepository
    {
        public SubscriptionPromotionSeriesBonusRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

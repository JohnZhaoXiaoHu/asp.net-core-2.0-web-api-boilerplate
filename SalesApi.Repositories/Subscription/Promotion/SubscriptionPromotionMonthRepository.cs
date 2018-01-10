using Infrastructure.Features.Data;
using SalesApi.Models.Subscription.Promotion;

namespace SalesApi.Repositories.Subscription.Promotion
{
    public interface ISubscriptionPromotionMonthRepository: IEntityBaseRepository<SubscriptionPromotionMonth> { }

    public class SubscriptionPromotionMonthRepository: EntityBaseRepository<SubscriptionPromotionMonth>, ISubscriptionPromotionMonthRepository
    {
        public SubscriptionPromotionMonthRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

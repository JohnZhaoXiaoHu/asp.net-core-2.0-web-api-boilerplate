using Infrastructure.Features.Data;
using SalesApi.Models.Subscription.Order;

namespace SalesApi.Repositories.Subscription.Order
{
    public interface ISubscriptionOrderModifiedBonusDateRepository: IEntityBaseRepository<SubscriptionOrderModifiedBonusDate> { }

    public class SubscriptionOrderModifiedBonusDateRepository: EntityBaseRepository<SubscriptionOrderModifiedBonusDate>, ISubscriptionOrderModifiedBonusDateRepository
    {
        public SubscriptionOrderModifiedBonusDateRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

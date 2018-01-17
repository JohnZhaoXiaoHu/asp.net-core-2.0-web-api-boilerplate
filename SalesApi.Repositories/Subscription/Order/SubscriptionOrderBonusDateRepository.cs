using Infrastructure.Features.Data;
using SalesApi.Models.Subscription.Order;

namespace SalesApi.Repositories.Subscription.Order
{
    public interface ISubscriptionOrderBonusDateRepository: IEntityBaseRepository<SubscriptionOrderBonusDate> { }

    public class SubscriptionOrderBonusDateRepository: EntityBaseRepository<SubscriptionOrderBonusDate>, ISubscriptionOrderBonusDateRepository
    {
        public SubscriptionOrderBonusDateRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

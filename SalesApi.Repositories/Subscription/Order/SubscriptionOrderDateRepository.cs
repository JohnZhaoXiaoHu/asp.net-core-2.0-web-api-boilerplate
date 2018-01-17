using Infrastructure.Features.Data;
using SalesApi.Models.Subscription.Order;

namespace SalesApi.Repositories.Subscription.Order
{
    public interface ISubscriptionOrderDateRepository: IEntityBaseRepository<SubscriptionOrderDate> { }

    public class SubscriptionOrderDateRepository: EntityBaseRepository<SubscriptionOrderDate>, ISubscriptionOrderDateRepository
    {
        public SubscriptionOrderDateRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

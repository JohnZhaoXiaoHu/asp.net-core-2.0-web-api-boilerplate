using Infrastructure.Features.Data;
using SalesApi.Models.Subscription.Order;

namespace SalesApi.Repositories.Subscription.Order
{
    public interface ISubscriptionOrderRepository : IEntityBaseRepository<SubscriptionOrder>
    {
        
    }

    public class SubscriptionOrderRepository: EntityBaseRepository<SubscriptionOrder>, ISubscriptionOrderRepository
    {
        public SubscriptionOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

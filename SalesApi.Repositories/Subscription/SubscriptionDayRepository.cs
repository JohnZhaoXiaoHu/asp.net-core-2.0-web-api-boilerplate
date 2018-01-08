using Infrastructure.Features.Data;
using SalesApi.Models.Subscription;

namespace SalesApi.Repositories.Subscription
{
    public interface ISubscriptionDayRepository: IEntityBaseRepository<SubscriptionDay> { }

    public class SubscriptionDayRepository: EntityBaseRepository<SubscriptionDay>, ISubscriptionDayRepository
    {
        public SubscriptionDayRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

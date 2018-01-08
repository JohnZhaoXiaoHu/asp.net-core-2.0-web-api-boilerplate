using System.Linq;
using Infrastructure.Features.Data;
using SalesApi.Models.Subscription;

namespace SalesApi.Repositories.Subscription
{
    public interface ISubscriptionPromotionSeriesRepository : IEntityBaseRepository<SubscriptionPromotionSeries>
    {

    }

    public class SubscriptionPromotionSeriesRepository : EntityBaseRepository<SubscriptionPromotionSeries>, ISubscriptionPromotionSeriesRepository
    {
        public SubscriptionPromotionSeriesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

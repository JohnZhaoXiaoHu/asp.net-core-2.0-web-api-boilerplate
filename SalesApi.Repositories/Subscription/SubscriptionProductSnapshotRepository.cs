using System;
using Infrastructure.Features.Data;
using SalesApi.Models.Subscription;

namespace SalesApi.Repositories.Subscription
{
    public interface ISubscriptionProductSnapshotRepository : IEntityBaseRepository<SubscriptionProductSnapshot>
    {
    }

    public class SubscriptionProductSnapshotRepository : EntityBaseRepository<SubscriptionProductSnapshot>, ISubscriptionProductSnapshotRepository
    {
        public SubscriptionProductSnapshotRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

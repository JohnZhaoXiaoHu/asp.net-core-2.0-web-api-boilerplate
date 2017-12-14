using Infrastructure.Features.Data;
using SalesApi.Models.Collective;

namespace SalesApi.Repositories.Collective
{
    public interface ICollectiveProductSnapshotRepository : IEntityBaseRepository<CollectiveProductSnapshot>
    {

    }

    public class CollectiveProductSnapshotRepository : EntityBaseRepository<CollectiveProductSnapshot>, ICollectiveProductSnapshotRepository
    {
        public CollectiveProductSnapshotRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

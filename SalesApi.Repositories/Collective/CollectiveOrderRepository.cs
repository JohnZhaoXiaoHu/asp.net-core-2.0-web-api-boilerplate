using Infrastructure.Features.Data;
using SalesApi.Models.Collective;

namespace SalesApi.Repositories.Collective
{
    public interface ICollectiveOrderRepository : IEntityBaseRepository<CollectiveOrder>
    {
        
    }

    public class CollectiveOrderRepository: EntityBaseRepository<CollectiveOrder>, ICollectiveOrderRepository
    {
        public CollectiveOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

using Infrastructure.Features.Data;
using SalesApi.Models.Collective;

namespace SalesApi.Repositories.Collective
{
    public interface ICollectiveDayRepository : IEntityBaseRepository<CollectiveDay>
    {
        
    }

    public class CollectiveDayRepository: EntityBaseRepository<CollectiveDay>, ICollectiveDayRepository
    {
        public CollectiveDayRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

using Infrastructure.Features.Data;
using SalesApi.Models.Collective;

namespace SalesApi.Repositories.Collective
{
    public interface ICollectivePriceRepository : IEntityBaseRepository<CollectivePrice>
    {

    }

    public class CollectivePriceRepository : EntityBaseRepository<CollectivePrice>, ICollectivePriceRepository
    {
        public CollectivePriceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

using Infrastructure.Features.Data;
using SalesApi.Models.Retail;

namespace SalesApi.Repositories.Retail
{
    public interface IRetailProductSnapshotRepository : IEntityBaseRepository<RetailProductSnapshot>
    {

    }

    public class RetailProductSnapshotRepository : EntityBaseRepository<RetailProductSnapshot>, IRetailProductSnapshotRepository
    {
        public RetailProductSnapshotRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

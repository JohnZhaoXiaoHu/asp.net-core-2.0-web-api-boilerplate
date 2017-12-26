using Infrastructure.Features.Data;
using SalesApi.Models.County;

namespace SalesApi.Repositories.County
{
    public interface ICountyProductSnapshotRepository: IEntityBaseRepository<CountyProductSnapshot> { }

    public class CountyProductSnapshotRepository: EntityBaseRepository<CountyProductSnapshot>, ICountyProductSnapshotRepository
    {
        public CountyProductSnapshotRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

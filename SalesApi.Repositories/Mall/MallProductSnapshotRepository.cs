using Infrastructure.Features.Data;
using SalesApi.Models.Mall;

namespace SalesApi.Repositories.Mall
{
    public interface IMallProductSnapshotRepository: IEntityBaseRepository<MallProductSnapshot> { }

    public class MallProductSnapshotRepository: EntityBaseRepository<MallProductSnapshot>, IMallProductSnapshotRepository
    {
        public MallProductSnapshotRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

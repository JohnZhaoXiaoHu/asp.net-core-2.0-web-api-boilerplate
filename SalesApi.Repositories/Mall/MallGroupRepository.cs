using Infrastructure.Features.Data;
using SalesApi.Models.Mall;

namespace SalesApi.Repositories.Mall
{
    public interface IMallGroupRepository: IEntityBaseRepository<MallGroup> { }

    public class MallGroupRepository: EntityBaseRepository<MallGroup>, IMallGroupRepository
    {
        public MallGroupRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

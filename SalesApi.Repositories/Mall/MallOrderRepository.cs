using Infrastructure.Features.Data;
using SalesApi.Models.Mall;

namespace SalesApi.Repositories.Mall
{
    public interface IMallOrderRepository: IEntityBaseRepository<MallOrder> { }

    public class MallOrderRepository: EntityBaseRepository<MallOrder>, IMallOrderRepository
    {
        public MallOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

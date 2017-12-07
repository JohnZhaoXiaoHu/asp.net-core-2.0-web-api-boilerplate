using Infrastructure.Features.Data;
using SalesApi.Models.Overall;

namespace SalesApi.Repositories.Overall
{
    public interface ISalesDayRepository : IEntityBaseRepository<SalesDay>
    {

    }

    public class SalesDayRepository : EntityBaseRepository<SalesDay>, ISalesDayRepository
    {
        public SalesDayRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

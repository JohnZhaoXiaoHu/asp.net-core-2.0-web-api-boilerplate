using Infrastructure.Features.Data;
using SalesApi.Models.Retail;

namespace SalesApi.Repositories.Retail
{
    public interface IRetailOrderRepository : IEntityBaseRepository<RetailOrder>
    {

    }

    public class RetailOrderRepository : EntityBaseRepository<RetailOrder>, IRetailOrderRepository
    {
        public RetailOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

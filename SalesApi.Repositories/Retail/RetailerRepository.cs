using Infrastructure.Features.Data;
using SalesApi.Models.Retail;

namespace SalesApi.Repositories.Retail
{
    public interface IRetailerRepository : IEntityBaseRepository<Retailer> { }

    public class RetailerRepository : EntityBaseRepository<Retailer>, IRetailerRepository
    {
        public RetailerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

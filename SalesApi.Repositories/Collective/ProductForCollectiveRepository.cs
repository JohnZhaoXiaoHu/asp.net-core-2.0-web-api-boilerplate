using Infrastructure.Features.Data;
using SalesApi.Models.Collective;

namespace SalesApi.Repositories.Collective
{
    public interface IProductForCollectiveRepository : IEntityBaseRepository<ProductForCollective>
    {
        
    }

    public class ProductForCollectiveRepository: EntityBaseRepository<ProductForCollective>, IProductForCollectiveRepository
    {
        public ProductForCollectiveRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

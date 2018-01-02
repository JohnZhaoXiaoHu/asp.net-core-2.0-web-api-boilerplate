using Infrastructure.Features.Data;
using SalesApi.Models.Mall;

namespace SalesApi.Repositories.Mall
{
    public interface IProductForMallRepository: IEntityBaseRepository<ProductForMall> { }

    public class ProductForMallRepository: EntityBaseRepository<ProductForMall>, IProductForMallRepository
    {
        public ProductForMallRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

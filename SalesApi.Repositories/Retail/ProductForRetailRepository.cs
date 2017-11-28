using Infrastructure.Features.Data;
using SalesApi.Models.Retail;

namespace SalesApi.Repositories.Retail
{
    public interface IProductForRetailRepository: IEntityBaseRepository<ProductForRetail> { }

    public class ProductForRetailRepository: EntityBaseRepository<ProductForRetail>, IProductForRetailRepository
    {
        public ProductForRetailRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

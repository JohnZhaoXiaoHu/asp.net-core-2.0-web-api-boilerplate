using Infrastructure.Features.Data;
using SalesApi.Models.Settings;

namespace SalesApi.Repositories.Settings
{
    public interface IProductPriceRepository: IEntityBaseRepository<ProductPrice> { }

    public class ProductPriceRepository: EntityBaseRepository<ProductPrice>, IProductPriceRepository
    {
        public ProductPriceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

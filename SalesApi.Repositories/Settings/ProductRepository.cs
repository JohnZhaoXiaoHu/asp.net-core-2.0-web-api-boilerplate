using Infrastructure.Features.Data;
using SalesApi.Models.Settings;

namespace SalesApi.Repositories.Settings
{
    public interface IProductRepository: IEntityBaseRepository<Product> { }
    public class ProductRepository: EntityBaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

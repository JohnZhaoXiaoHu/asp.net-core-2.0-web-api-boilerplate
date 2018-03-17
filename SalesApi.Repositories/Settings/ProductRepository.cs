using SalesApi.Infrastructure.Abstractions.Data;
using SalesApi.Infrastructure.DomainModels;
using SalesApi.Infrastructure.IRepositories.Settings;

namespace SalesApi.Repositories.Settings
{
    public class ProductRepository : EntityBaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

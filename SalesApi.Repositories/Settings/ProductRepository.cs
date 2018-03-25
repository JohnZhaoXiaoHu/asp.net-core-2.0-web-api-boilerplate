using SalesApi.Core.Abstractions.Data;
using SalesApi.Core.DomainModels;
using SalesApi.Core.IRepositories.Settings;

namespace SalesApi.Repositories.Settings
{
    public class ProductRepository : EntityBaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

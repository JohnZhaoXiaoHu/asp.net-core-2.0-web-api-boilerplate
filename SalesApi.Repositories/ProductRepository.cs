using SalesApi.Core.Abstractions.Data;
using SalesApi.Core.DomainModels;
using SalesApi.Core.IRepositories;

namespace SalesApi.Repositories
{
    public class ProductRepository : EntityBaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

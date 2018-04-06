using SalesApi.Core.Abstractions.Data;
using SalesApi.Core.DomainModels;

namespace SalesApi.Core.IRepositories
{
    public interface IProductRepository : IEntityBaseRepository<Product>
    {
    }
}
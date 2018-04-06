using SalesApi.Core.IRepositories;
using SalesApi.Core.IServices;

namespace SalesApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
    }
}

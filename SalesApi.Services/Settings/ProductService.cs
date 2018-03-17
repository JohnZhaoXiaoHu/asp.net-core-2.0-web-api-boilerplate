using SalesApi.Infrastructure.IRepositories.Settings;
using SalesApi.Infrastructure.IServices.Settings;

namespace SalesApi.Services.Settings
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

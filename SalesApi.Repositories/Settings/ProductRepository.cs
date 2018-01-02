using Infrastructure.Features.Data;
using SalesApi.Models.Settings;
using SharedSettings.Tools;

namespace SalesApi.Repositories.Settings
{
    public interface IProductRepository : IEntityBaseRepository<Product>
    {
        void SetPinyin(Product product);
    }

    public class ProductRepository : EntityBaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void SetPinyin(Product product)
        {
            product.Pinyin = PinyinTool.GetPinyin(product.Name);
            product.FullPinyin = PinyinTool.GetPinyin(product.FullName);
        }
    }
}

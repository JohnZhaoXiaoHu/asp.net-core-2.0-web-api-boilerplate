using System;
using Infrastructure.Features.Data;
using SalesApi.Models.Retail;

namespace SalesApi.Repositories.Retail
{
    public interface IProductForRetailRepository: IEntityBaseRepository<ProductForRetail> {
        void SetPrice(ProductForRetail model);
    }

    public class ProductForRetailRepository: EntityBaseRepository<ProductForRetail>, IProductForRetailRepository
    {
        public ProductForRetailRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void SetPrice(ProductForRetail model)
        {
            if (model.IsOrderByBox)
            {
                if (model.BoxPrice == 0)
                {
                    throw new Exception("整箱报货的整箱价不能为0");
                }
                model.Price = Math.Round(model.BoxPrice / model.EquivalentBox, 2);
            }
            else
            {
                model.BoxPrice = Math.Round(model.Price * model.EquivalentBox, 2);
            }
        }
    }
}

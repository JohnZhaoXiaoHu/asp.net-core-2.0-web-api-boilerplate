using System;
using Infrastructure.Features.Data;
using SalesApi.Models.Subscription;

namespace SalesApi.Repositories.Subscription
{
    public interface IProductForSubscriptionRepository: IEntityBaseRepository<ProductForSubscription>
    {
        void SetPrice(ProductForSubscription model);
    }

    public class ProductForSubscriptionRepository: EntityBaseRepository<ProductForSubscription>, IProductForSubscriptionRepository
    {
        public ProductForSubscriptionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void SetPrice(ProductForSubscription model)
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

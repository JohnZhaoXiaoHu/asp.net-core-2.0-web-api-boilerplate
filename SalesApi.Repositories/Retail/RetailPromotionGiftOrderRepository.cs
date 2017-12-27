using Infrastructure.Features.Data;
using SalesApi.Models.Retail;

namespace SalesApi.Repositories.Retail
{
    public interface IRetailPromotionGiftOrderRepository : IEntityBaseRepository<RetailPromotionGiftOrder> { }

    public class RetailPromotionGiftOrderRepository: EntityBaseRepository<RetailPromotionGiftOrder>, IRetailPromotionGiftOrderRepository
    {
        public RetailPromotionGiftOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

using Infrastructure.Features.Data;
using SalesApi.Models.County;

namespace SalesApi.Repositories.County
{
    public interface ICountyPromotionGiftOrderRepository : IEntityBaseRepository<CountyPromotionGiftOrder> { }

    public class CountyPromotionGiftOrderRepository : EntityBaseRepository<CountyPromotionGiftOrder>, ICountyPromotionGiftOrderRepository
    {
        public CountyPromotionGiftOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

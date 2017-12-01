using Infrastructure.Features.Data;
using SalesApi.Models.Retail;

namespace SalesApi.Repositories.Retail
{
    public interface IRetailPromotionEventBonusRepository : IEntityBaseRepository<RetailPromotionEventBonus>
    {

    }

    public class RetailPromotionEventBonusRepository : EntityBaseRepository<RetailPromotionEventBonus>, IRetailPromotionEventBonusRepository
    {
        public RetailPromotionEventBonusRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

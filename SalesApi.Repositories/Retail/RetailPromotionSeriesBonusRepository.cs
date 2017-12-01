using Infrastructure.Features.Data;
using SalesApi.Models.Retail;

namespace SalesApi.Repositories.Retail
{
    public interface IRetailPromotionSeriesBonusRepository : IEntityBaseRepository<RetailPromotionSeriesBonus>
    {

    }

    public class RetailPromotionSeriesBonusRepository : EntityBaseRepository<RetailPromotionSeriesBonus>, IRetailPromotionSeriesBonusRepository
    {
        public RetailPromotionSeriesBonusRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

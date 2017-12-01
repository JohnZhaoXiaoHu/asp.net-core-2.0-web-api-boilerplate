using Infrastructure.Features.Data;
using SalesApi.Models.Retail;

namespace SalesApi.Repositories.Retail
{
    public interface IRetailPromotionSeriesRepository : IEntityBaseRepository<RetailPromotionSeries>
    {

    }

    public class RetailPromotionSeriesRepository : EntityBaseRepository<RetailPromotionSeries>, IRetailPromotionSeriesRepository
    {
        public RetailPromotionSeriesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

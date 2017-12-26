using Infrastructure.Features.Data;
using SalesApi.Models.County;

namespace SalesApi.Repositories.County
{
    public interface ICountyPromotionSeriesRepository : IEntityBaseRepository<CountyPromotionSeries> { }

    public class CountyPromotionSeriesRepository: EntityBaseRepository<CountyPromotionSeries>, ICountyPromotionSeriesRepository
    {
        public CountyPromotionSeriesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

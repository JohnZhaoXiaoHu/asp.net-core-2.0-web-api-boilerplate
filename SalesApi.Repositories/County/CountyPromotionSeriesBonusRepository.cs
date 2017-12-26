using Infrastructure.Features.Data;
using SalesApi.Models.County;

namespace SalesApi.Repositories.County
{
    public interface ICountyPromotionSeriesBonusRepository: IEntityBaseRepository<CountyPromotionSeriesBonus> { }

    public class CountyPromotionSeriesBonusRepository: EntityBaseRepository<CountyPromotionSeriesBonus>, ICountyPromotionSeriesBonusRepository
    {
        public CountyPromotionSeriesBonusRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

using Infrastructure.Features.Data;
using SalesApi.Models.County;

namespace SalesApi.Repositories.County
{
    public interface ICountyPromotionEventBonusRepository: IEntityBaseRepository<CountyPromotionEventBonus> { }

    public class CountyPromotionEventBonusRepository: EntityBaseRepository<CountyPromotionEventBonus>, ICountyPromotionEventBonusRepository
    {
        public CountyPromotionEventBonusRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

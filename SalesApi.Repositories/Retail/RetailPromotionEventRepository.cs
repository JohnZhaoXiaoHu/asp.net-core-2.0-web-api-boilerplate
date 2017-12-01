using Infrastructure.Features.Data;
using SalesApi.Models.Retail;

namespace SalesApi.Repositories.Retail
{
    public interface IRetailPromotionEventRepository : IEntityBaseRepository<RetailPromotionEvent>
    {

    }

    public class RetailPromotionEventRepository : EntityBaseRepository<RetailPromotionEvent>, IRetailPromotionEventRepository
    {
        public RetailPromotionEventRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

using Infrastructure.Features.Data;
using SalesApi.Models.Subscription.Promotion;

namespace SalesApi.Repositories.Subscription.Promotion
{
    public interface ISubscriptionPromotionMonthBonusDeliveryDateRepository
        : IEntityBaseRepository<SubscriptionPromotionMonthBonusDeliveryDate>
    {

    }

    public class SubscriptionPromotionMonthBonusDeliveryDateRepository 
        : EntityBaseRepository<SubscriptionPromotionMonthBonusDeliveryDate>, ISubscriptionPromotionMonthBonusDeliveryDateRepository
    {
        public SubscriptionPromotionMonthBonusDeliveryDateRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

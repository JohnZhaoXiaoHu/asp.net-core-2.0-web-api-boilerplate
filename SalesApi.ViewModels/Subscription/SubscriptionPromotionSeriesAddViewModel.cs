using System.Collections.Generic;
using FluentValidation;

namespace SalesApi.ViewModels.Subscription
{
    public class SubscriptionPromotionSeriesAddViewModel : SubscriptionPromotionSeriesViewModel
    {
        public List<SubscriptionPromotionSeriesBonusViewModel> SubscriptionPromotionSeriesBonuses { get; set; }
    }

    public class SubscriptionPromotionSeriesAddViewModelValidator : AbstractValidator<SubscriptionPromotionSeriesAddViewModel>
    {
        public SubscriptionPromotionSeriesAddViewModelValidator()
        {
            RuleFor(x => x.StartDate).LessThanOrEqualTo(x => x.EndDate).WithMessage("开始日期不能大于结束日期");
        }
    }
}

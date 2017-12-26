using System.Collections.Generic;
using FluentValidation;

namespace SalesApi.ViewModels.County
{
    public class CountyPromotionSeriesAddViewModel : CountyPromotionSeriesViewModel
    {
        public List<CountyPromotionSeriesBonusViewModel> CountyPromotionSeriesBonuses { get; set; }
    }

    public class CountyPromotionSeriesAddViewModelValidator : AbstractValidator<CountyPromotionSeriesAddViewModel>
    {
        public CountyPromotionSeriesAddViewModelValidator()
        {
            RuleFor(x => x.StartDate).LessThanOrEqualTo(x => x.EndDate).WithMessage("开始日期不能大于结束日期");
        }
    }
}

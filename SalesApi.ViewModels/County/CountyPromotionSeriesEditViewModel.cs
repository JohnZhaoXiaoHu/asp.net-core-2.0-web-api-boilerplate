using System.Collections.Generic;
using FluentValidation;

namespace SalesApi.ViewModels.County
{
    public class CountyPromotionSeriesEditViewModel : CountyPromotionSeriesViewModel
    {
        public List<CountyPromotionSeriesBonusViewModel> CountyPromotionSeriesBonuses { get; set; }
    }

    public class CountyPromotionSeriesEditViewModelValidator : AbstractValidator<CountyPromotionSeriesEditViewModel>
    {
        public CountyPromotionSeriesEditViewModelValidator()
        {
            RuleFor(x => x.StartDate).LessThanOrEqualTo(x => x.EndDate).WithMessage("开始日期不能大于结束日期");
        }
    }
}

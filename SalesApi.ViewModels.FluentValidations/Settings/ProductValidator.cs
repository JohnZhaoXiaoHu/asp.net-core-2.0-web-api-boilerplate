using FluentValidation;
using SalesApi.ViewModels.Settings;

namespace SalesApi.ViewModels.FluentValidations.Settings
{
    public class ProductValidator : AbstractValidator<ProductCreationViewModel>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(10).WithName("Product Name")
                .WithMessage("Please specify a {PropertyName}, And the length should be less than {MaximumLength}");
            RuleFor(p => p.FullName).MaximumLength(50);
            RuleFor(p => p.EquivalentTon).GreaterThan(0).WithMessage("{PropertyName} should greater than {GreaterThan}");
            RuleFor(p => p.TaxRate).GreaterThanOrEqualTo(0).WithMessage("{PropertyName} should greater than or equal to {GreaterThan}");
        }
    }
}

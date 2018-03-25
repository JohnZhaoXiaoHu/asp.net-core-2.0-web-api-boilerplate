using FluentValidation;
using SalesApi.Core.Abstractions.DomainModels;
using SalesApi.Shared.Enums;

namespace SalesApi.ViewModels.Settings
{
    public class ProductCreationViewModel: IOrder
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Specification { get; set; }
        public ProductUnit ProductUnit { get; set; }
        public int ShelfLife { get; set; }
        public decimal EquivalentTon { get; set; }
        public string Barcode { get; set; }
        public decimal TaxRate { get; set; }
        public int Order { get; set; }
    }

    public class ProductCreationValidator : AbstractValidator<ProductCreationViewModel>
    {
        public ProductCreationValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(10).WithName("Product Name")
                .WithMessage("Please specify a {PropertyName}, And the length should be less than {MaximumLength}");
            RuleFor(p => p.FullName).MaximumLength(50);
            RuleFor(p => p.EquivalentTon).GreaterThan(0).WithMessage("{PropertyName} should greater than {GreaterThan}");
            RuleFor(p => p.TaxRate).GreaterThanOrEqualTo(0).WithMessage("{PropertyName} should greater than or equal to {GreaterThan}");
        }
    }
}

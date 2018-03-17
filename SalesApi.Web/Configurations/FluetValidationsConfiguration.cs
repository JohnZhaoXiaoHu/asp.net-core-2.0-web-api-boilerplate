using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using SalesApi.ViewModels.Settings;

namespace SalesApi.Web.Configurations
{
    public static class FluetValidationsConfiguration
    {
        public static void AddFluetValidations(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductCreationValidator>());
        }
    }
}

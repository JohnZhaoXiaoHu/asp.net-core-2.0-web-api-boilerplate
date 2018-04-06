using SalesApi.Core.Abstractions.DomainModels;

namespace SalesApi.ViewModels
{
    public class CustomerViewModel: EntityBase
    {
        public string Company { get; set; }
        public string Name { get; set; }
    }
}
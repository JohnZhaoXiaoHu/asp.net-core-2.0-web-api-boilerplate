using SalesApi.Core.Abstractions.DomainModels;

namespace SalesApi.Core.DomainModels
{
    public class Customer: EntityBase
    {
        public string Company { get; set; }
        public string Name { get; set; }
    }
}
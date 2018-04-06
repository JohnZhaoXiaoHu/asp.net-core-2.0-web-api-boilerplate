using SalesApi.Core.Abstractions.DomainModels;

namespace SalesApi.Core.DomainModels
{
    public class Vehicle: EntityBase
    {
        public string Model { get; set; }
        public string Owner { get; set; }
    }
}
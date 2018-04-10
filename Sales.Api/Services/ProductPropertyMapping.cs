using System;
using System.Collections.Generic;
using Sales.Infrastructure.UsefulModels.Pagination;

namespace Sales.Api.Services
{
    public class ProductPropertyMapping : PropertyMapping
    {
        public ProductPropertyMapping() : base(
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Genre", new PropertyMappingValue(new [] { "Genre" } )},
                    { "Age", new PropertyMappingValue(new [] { "DateOfBirth" } , true) },
                    { "Name", new PropertyMappingValue(new [] { "FirstName", "LastName" }) }
                })
        {
        }
    }
}

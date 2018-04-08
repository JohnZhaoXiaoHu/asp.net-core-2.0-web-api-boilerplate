using System.Collections.Generic;

namespace Sales.Core.Bases.Pagination
{
    public interface IPropertyMappingService
    {        
        bool ValidMappingExistsFor<TSource, TDestination>(string fields);

        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
    }
}
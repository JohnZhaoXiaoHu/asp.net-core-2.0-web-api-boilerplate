using System;
using System.Collections.Generic;
using System.Linq;
using SalesApi.Core.Abstractions.Data.PagingRelated;
using SalesApi.Core.DomainModels;
using SalesApi.Core.IServices;
using SalesApi.ViewModels;

namespace SalesApi.Services
{
    public class CustomerPropertyMappingService : ICustomerPropertyMappingService
    {
        private readonly Dictionary<string, PropertyMappingValue> _propertyMapping =
              new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
              {
               { "Id", new PropertyMappingValue(new List<string> { "Id" } ) },
               { "Company", new PropertyMappingValue(new List<string> { "Company" } )},
               { "Age", new PropertyMappingValue(new List<string> { "EstablishmentTime" } , true) },
               { "Name", new PropertyMappingValue(new List<string> { "Name" }) }
              };

        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public CustomerPropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<CustomerViewModel, Customer>(_propertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>().ToList();

            if (matchingMapping.Count == 1)
            {
                return matchingMapping.First().MappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance for <{typeof(TSource)},{typeof(TDestination)}");
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            // the string is separated by ",", so we split it.
            var fieldsAfterSplit = fields.Split(',');

            // run through the fields clauses
            foreach (var field in fieldsAfterSplit)
            {
                // trim
                var trimmedField = field.Trim();

                // remove everything after the first " " - if the fields 
                // are coming from an orderBy string, this part must be 
                // ignored
                var indexOfFirstSpace = trimmedField.IndexOf(" ", StringComparison.Ordinal);
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedField : trimmedField.Remove(indexOfFirstSpace);

                // find the matching property
                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;

        }
    }
}

using Microsoft.AspNetCore.Hosting.Internal;
using SalesApi.Core.DomainModels;
using SalesApi.Core.IRepositories;
using SalesApi.Core.IServices;
using SalesApi.Shared.UsefulModels;

namespace SalesApi.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerPropertyMappingService _propertyMappingService;

        public CustomerService(
            ICustomerRepository customerRepository,
            ICustomerPropertyMappingService propertyMappingService)
        {
            _customerRepository = customerRepository;
            _propertyMappingService = propertyMappingService;
        }

        //public PaginatedItems<Customer> GetByPage(CustomerParameters parameters)
        //{
        //    var collectionBeforePaging =
        //        _customerRepository.Context.ApplySort(parameters.OrderBy,
        //            _propertyMappingService.GetPropertyMapping<AuthorDto, Author>());
        //}
    }
}

using SalesApi.Core.Abstractions.Data;
using SalesApi.Core.DomainModels;
using SalesApi.Core.Helpers;
using SalesApi.Core.IRepositories;
using SalesApi.Core.IServices;
using SalesApi.Shared.UsefulModels;

namespace SalesApi.Repositories
{
    public class CustomerRepository : EntityBaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
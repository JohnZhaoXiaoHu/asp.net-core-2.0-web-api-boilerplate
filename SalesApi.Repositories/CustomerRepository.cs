using SalesApi.Core.Abstractions.Data;
using SalesApi.Core.DomainModels;
using SalesApi.Core.IRepositories;

namespace SalesApi.Repositories
{
    public class CustomerRepository : EntityBaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
using Infrastructure.Features.Data;
using SalesApi.Models.Collective;
using SharedSettings.Tools;

namespace SalesApi.Repositories.Collective
{
    public interface ICollectiveCustomerRepository : IEntityBaseRepository<CollectiveCustomer>
    {
        void SetPinyin(CollectiveCustomer customer);
    }

    public class CollectiveCustomerRepository : EntityBaseRepository<CollectiveCustomer>, ICollectiveCustomerRepository
    {
        public CollectiveCustomerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void SetPinyin(CollectiveCustomer customer)
        {
            customer.Pinyin = PinyinTool.GetPinyin(customer.Name);
        }
    }
}

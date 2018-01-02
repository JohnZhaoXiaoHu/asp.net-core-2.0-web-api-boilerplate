using Infrastructure.Features.Data;
using SalesApi.Models.Mall;
using SharedSettings.Tools;

namespace SalesApi.Repositories.Mall
{
    public interface IMallCustomerRepository : IEntityBaseRepository<MallCustomer>
    {
        void SetPinyin(MallCustomer customer);
    }

    public class MallCustomerRepository : EntityBaseRepository<MallCustomer>, IMallCustomerRepository
    {
        public MallCustomerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void SetPinyin(MallCustomer customer)
        {
            customer.Pinyin = PinyinTool.GetPinyin(customer.Name);
        }
    }
}

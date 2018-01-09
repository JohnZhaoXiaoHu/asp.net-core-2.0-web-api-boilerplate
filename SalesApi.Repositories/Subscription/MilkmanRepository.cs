using Infrastructure.Features.Data;
using SalesApi.Models.Subscription;
using SharedSettings.Tools;

namespace SalesApi.Repositories.Subscription
{
    public interface IMilkmanRepository : IEntityBaseRepository<Milkman>
    {
        void SetPinyin(Milkman customer);
    }

    public class MilkmanRepository : EntityBaseRepository<Milkman>, IMilkmanRepository
    {
        public MilkmanRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void SetPinyin(Milkman customer)
        {
            customer.Pinyin = PinyinTool.GetPinyin(customer.Name);
        }
    }
}

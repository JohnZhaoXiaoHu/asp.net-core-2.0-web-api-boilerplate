using Infrastructure.Features.Data;
using SalesApi.Models.County;
using SharedSettings.Tools;

namespace SalesApi.Repositories.County
{
    public interface ICountyAgentRepository: IEntityBaseRepository<CountyAgent>
    {
        void SetPinyin(CountyAgent customer);
    }

    public class CountyAgentRepository: EntityBaseRepository<CountyAgent>, ICountyAgentRepository
    {
        public CountyAgentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void SetPinyin(CountyAgent customer)
        {
            customer.Pinyin = PinyinTool.GetPinyin(customer.Name);
        }
    }
}

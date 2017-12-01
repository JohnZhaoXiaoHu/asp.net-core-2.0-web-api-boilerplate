using Infrastructure.Features.Data;
using SalesApi.Models.Retail;
using SharedSettings.Tools;

namespace SalesApi.Repositories.Retail
{
    public interface IRetailerRepository : IEntityBaseRepository<Retailer>
    {
        void SetPinyin(Retailer retailer);
    }

    public class RetailerRepository : EntityBaseRepository<Retailer>, IRetailerRepository
    {
        public RetailerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void SetPinyin(Retailer retailer)
        {
            retailer.Pinyin = PinyinTool.GetPinyin(retailer.Name);
        }
    }
}

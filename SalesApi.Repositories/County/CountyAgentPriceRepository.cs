using Infrastructure.Features.Data;
using SalesApi.Models.County;

namespace SalesApi.Repositories.County
{
    public interface ICountyAgentPriceRepository: IEntityBaseRepository<CountyAgentPrice> { }

    public class CountyAgentPriceRepository: EntityBaseRepository<CountyAgentPrice>, ICountyAgentPriceRepository
    {
        public CountyAgentPriceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

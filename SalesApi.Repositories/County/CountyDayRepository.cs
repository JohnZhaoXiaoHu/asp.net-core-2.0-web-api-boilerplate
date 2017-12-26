using Infrastructure.Features.Data;
using SalesApi.Models.County;

namespace SalesApi.Repositories.County
{
    public interface ICountyDayRepository: IEntityBaseRepository<CountyDay> { }

    public class CountyDayRepository: EntityBaseRepository<CountyDay>, ICountyDayRepository
    {
        public CountyDayRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

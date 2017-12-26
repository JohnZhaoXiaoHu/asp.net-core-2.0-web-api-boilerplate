using Infrastructure.Features.Data;
using SalesApi.Models.County;

namespace SalesApi.Repositories.County
{
    public interface ICountyOrderRepository: IEntityBaseRepository<CountyOrder> { }

    public class CountyOrderRepository: EntityBaseRepository<CountyOrder>, ICountyOrderRepository
    {
        public CountyOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

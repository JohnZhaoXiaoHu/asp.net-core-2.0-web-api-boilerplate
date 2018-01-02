using Infrastructure.Features.Data;
using SalesApi.Models.Mall;

namespace SalesApi.Repositories.Mall
{
    public interface IMallDayRepository: IEntityBaseRepository<MallDay> { }

    public class MallDayRepository: EntityBaseRepository<MallDay>, IMallDayRepository
    {
        public MallDayRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

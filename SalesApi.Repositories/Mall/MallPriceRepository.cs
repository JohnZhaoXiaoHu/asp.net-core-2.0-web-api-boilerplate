using Infrastructure.Features.Data;
using SalesApi.Models.Mall;

namespace SalesApi.Repositories.Mall
{
    public interface IMallPriceRepository: IEntityBaseRepository<MallPrice> { }

    public class MallPriceRepository: EntityBaseRepository<MallPrice>, IMallPriceRepository
    {
        public MallPriceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

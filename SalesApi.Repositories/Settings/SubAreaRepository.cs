using Infrastructure.Features.Data;
using SalesApi.Models.Settings;

namespace SalesApi.Repositories.Settings
{
    public interface ISubAreaRepository: IEntityBaseRepository<SubArea> { }

    public class SubAreaRepository: EntityBaseRepository<SubArea>, ISubAreaRepository
    {
        public SubAreaRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

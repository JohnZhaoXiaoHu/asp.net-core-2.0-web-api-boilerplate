using Infrastructure.Features.Data;
using SalesApi.Models.Settings;

namespace SalesApi.Repositories.Settings
{
    public interface IDistributionGroupRepository : IEntityBaseRepository<DistributionGroup> { }

    public class DistributionGroupRepository: EntityBaseRepository<DistributionGroup>, IDistributionGroupRepository
    {
        public DistributionGroupRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

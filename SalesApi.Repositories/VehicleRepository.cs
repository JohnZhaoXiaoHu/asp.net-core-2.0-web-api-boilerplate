using SalesApi.Core.Abstractions.Data;
using SalesApi.Core.DomainModels;
using SalesApi.Core.IRepositories;

namespace SalesApi.Repositories
{
    public class VehicleRepository : EntityBaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
using Infrastructure.Features.Data;
using SalesApi.Models.Settings;

namespace SalesApi.Repositories.Settings
{
    public interface IVehicleRepository : IEntityBaseRepository<Vehicle> { }

    public class VehicleRepository : EntityBaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

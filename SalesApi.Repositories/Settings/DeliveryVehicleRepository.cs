using Infrastructure.Features.Data;
using SalesApi.Models.Settings;

namespace SalesApi.Repositories.Settings
{
    public interface IDeliveryVehicleRepository: IEntityBaseRepository<DeliveryVehicle> { }

    public class DeliveryVehicleRepository: EntityBaseRepository<DeliveryVehicle>, IDeliveryVehicleRepository
    {
        public DeliveryVehicleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

using CoreApi.Models.Angular;
using Infrastructure.Features.Data;

namespace CoreApi.Repositories.Angular
{
    public interface IClientRepository : IEntityBaseRepository<Client> { }

    public class ClientRepository : EntityBaseRepository<Client>, IClientRepository
    {
        public ClientRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using CoreApi.DataContext.Infrastructure;
using CoreApi.Models.Angular;

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

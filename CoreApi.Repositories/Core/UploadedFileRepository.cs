using CoreApi.Models.Core;
using Infrastructure.Features.Data;

namespace CoreApi.Repositories.Core
{
    public interface IUploadedFileRepository: IEntityBaseRepository<UploadedFile> { }

    public class UploadedFileRepository: EntityBaseRepository<UploadedFile>, IUploadedFileRepository
    {
        public UploadedFileRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

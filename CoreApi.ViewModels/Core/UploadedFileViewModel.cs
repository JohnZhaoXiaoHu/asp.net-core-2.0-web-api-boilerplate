using Infrastructure.Features.Common;
using Infrastructure.Features.File;

namespace CoreApi.ViewModels.Core
{
    public class UploadedFileViewModel : EntityBase, IFileEntity
    {
        public int FileId
        {
            get { return Id; }
            set { }
        }

        public string FileName { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
    }
}
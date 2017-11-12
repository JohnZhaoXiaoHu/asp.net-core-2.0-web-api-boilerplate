using Infrastructure.Features.Common;

namespace CoreApi.ViewModels.Angular
{
    public class ClientViewModel : EntityBase
    {
        public decimal Balance { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }
}

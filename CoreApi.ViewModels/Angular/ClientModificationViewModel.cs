using System.ComponentModel.DataAnnotations;

namespace CoreApi.ViewModels.Angular
{
    public class ClientModificationViewModel
    {
        public decimal Balance { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Phone { get; set; }
    }
}

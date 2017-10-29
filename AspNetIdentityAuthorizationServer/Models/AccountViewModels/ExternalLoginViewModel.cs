using System.ComponentModel.DataAnnotations;

namespace AspNetIdentityAuthorizationServer.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

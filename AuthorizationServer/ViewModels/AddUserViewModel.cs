using System.ComponentModel.DataAnnotations;

namespace AuthorizationServer.ViewModels
{
    public class AddUserViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}

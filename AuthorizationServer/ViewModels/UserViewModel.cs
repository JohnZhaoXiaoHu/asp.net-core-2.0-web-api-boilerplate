using System.ComponentModel.DataAnnotations;

namespace AuthorizationServer.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Display(Name = "别名")]
        public string Alias { get; set; }
    }
}

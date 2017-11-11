using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "电话号码")]
        public string PhoneNumber { get; set; }

        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Display(Name = "登陆别名")]
        public string Alias { get; set; }

        [Display(Name = "锁定时间")]
        public DateTimeOffset? LockoutEnd { get; set; }

        [Display(Name = "是否启用锁定")]
        public bool LockoutEnabled { get; set; }

        [Display(Name = "登录失败次数")]
        public int AccessFailedCount { get; set; }
        
    }
}

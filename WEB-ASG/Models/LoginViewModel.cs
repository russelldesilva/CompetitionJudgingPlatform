using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_ASG.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email"), Required, EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Password"), Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

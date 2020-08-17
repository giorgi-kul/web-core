using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.AdminUI.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; internal set; }
        public string ReturnUrl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ProtonDb.Membership
{
    public class UserLogonModel
    {
        public UserLogonModel()
        {

        }

        public UserLogonModel(string userName, string pass)
        {
            this.UserName = userName;
            this.Password = pass;
        }

        [Required(ErrorMessage = "Kullanıcı Adı boş olamaz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre boş olamaz")]
        public string Password { get; set; }
    }
}

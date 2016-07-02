using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DuckyData1._0._0Alpha.ViewModels.Account
{
    public class User_Activation_Code
    {

        [Key]
        public int Id { get; set; }
        public string User_Account { get; set; }
        public string Activation_Code { get; set; }
        public string Password { get; set; }


        public User_Activation_Code() { }
        public User_Activation_Code(string account, string password, string code)
        {
            this.User_Account = account;
            this.Password = password;
            this.Activation_Code = code;
        }
    }

}
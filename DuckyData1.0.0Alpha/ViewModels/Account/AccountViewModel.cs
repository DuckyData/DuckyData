using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DuckyData1._0._0Alpha.ViewModels.Account
{
    public class userBase {
        [Display(Name = "First Name")]
        public string FirstName { set; get; }
        [Display(Name = "Last Name")]
        public string LastName { set; get; }
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { set; get; }
    }


    public class userAdd:userBase
    {
        [Key]
        public string Id { set; get; }
    }


}
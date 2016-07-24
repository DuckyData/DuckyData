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

    public class userRole : userBase
    {
        [Key]
        public string Id { set; get; }
        public string Role { set; get; }
    }

        public class userAdd:userBase
    {
        [Key]
        public string Id { set; get; }
    }

    public class adminEditUser :userBase
    {
        [Key]
        public string Id { set; get; }
        [Display(Name = "Phone Number")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$",ErrorMessage = "Please enter valid phone number.")]
        public string PhoneNumber { set; get; }
    }

    public class Flags
    {
        [Key]
        public string Id { set; get; }
        public string flagged { get; set; }
        public string gagged { get; set; }
        public string banned { get; set; }
    }

}
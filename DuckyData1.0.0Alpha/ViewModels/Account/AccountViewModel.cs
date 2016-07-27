using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DuckyData1._0._0Alpha.ViewModels.Account
{
    public class userBase {
        [Display(Name = "First Name")]
        [StringLength(20)]
        public string FirstName { set; get; }
        [Display(Name = "Last Name")]
        [StringLength(20)]
        public string LastName { set; get; }
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { set; get; }
    }

    public class userFlags: userBase
    {
        [Key]
        public string Id { set; get; }
   
        public bool flagged { set; get; }
        public bool gagged { set; get; }
        public bool banned { set; get; }
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
        [RegularExpression("/^[0-9]{10}$/",ErrorMessage = "Please enter valid phone number.")]
        public string PhoneNumber { set; get; }
        public bool flagged { set; get; }
        public bool gagged { set; get; }
        public bool banned { set; get; }
    }

    public class Flags
    {
        [Key]
        public string Id { set; get; }
        public bool flagged { get; set; }
        public bool gagged { get; set; }
        public bool banned { get; set; }
    }

}